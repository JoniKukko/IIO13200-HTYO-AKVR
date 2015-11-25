using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AKVR.Services.Train;
using AKVR.Services.TrafficLocation;
using System.Diagnostics;

public partial class Controllers_junat_junat : System.Web.UI.Page
{
    // Create new Services
    TrainService trainService = AKVR.Services.ServiceFactory.Train();
    TrafficLocationService trafficLocationService = AKVR.Services.ServiceFactory.TrafficLocation();

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btnSearchTrains_Click(object sender, EventArgs e)
    {
        searchTrains();
    }

    public Match isNumber(string query)
    {
        // Checks if only numbers, train number
        Regex regexTrainNumber = new Regex(@"^\d+$"); //^[0-9]{1,3}$
        Match check = regexTrainNumber.Match(query);

        return check;
    }

    /* 
    Check whether the search query is int or string,
    then uses the appropriate method to search trains.
    Calls a method to update the view.
    */
    private void searchTrains()
    {
        labelTrain.Text = "";
        dlTrains.Items.Clear();

        string query = tbSearchTrains.Text;

        // Check that the query is not empty
        if (query == "")
        {
            labelTrain.Text = "Anna hakusana.";
            return;
        }

        TrainModel resultTrain;
        List<TrainModel> resultTrainList;

        Match check = isNumber(query);

        // Determine the type of the query
        // check.Success == true when the query is numbers, train number
        // Otherwise expect string, station name
        if (check.Success)
        {
            // Query from text to int
            int intQuery;
            Int32.TryParse(query, out intQuery);

            resultTrain = searchTrainByNumber(intQuery);

            // Check if the returned train is empty
            // If not, update tableTrainResults
            if (resultTrain.trainType != null)
            {
                updateTrainInfo(resultTrain);
            }
            else
            {
                Debug.WriteLine("AKVR:junat.aspx.cs:searchTrains() - searchTrainByNumber(int) - returned empty train");
                labelTrain.Text = "Junia ei löytynyt.";
            }
            
        }
        // If search query is not number; station name
        else
        {
            resultTrainList = searchTrainsByStation(query);
            Session["resultTrainList"] = resultTrainList;
            Debug.WriteLine("AKVR:junat.aspx.cs:searchTrains() - " + resultTrainList.Count);


            if (resultTrainList.Count != 0 && resultTrainList[0].FullTrainName != "0")
            {
                // Display resulting trains in dropdown list
                dlTrains.DataSource = resultTrainList;
                dlTrains.DataValueField = "FullTrainName";
                dlTrains.DataBind();
                labelTrain.Text = "";

            } else
            {
                labelTrain.Text = "Junia ei löytynyt.";
                Debug.WriteLine("AKVR:junat.aspx.cs:searchTrains() - searchTrainsByStation() returned empty list");
            }
        }

        
    }

    private TrainModel searchTrainByNumber(int trainNumber)
    {
        // Search the train
        return trainService.SelectByTrainNumber(trainNumber);

    }

    private void updateTrainInfo(TrainModel resultTrain)
    {
        // Print train type and number in label
        labelTrain.Text = resultTrain.trainType.ToString() + resultTrain.trainNumber.ToString();

        // Timetable
        for (int i = 0; i < resultTrain.timeTableRows.Count() - 1; i++)
        {
            // If the train stops
            if (resultTrain.timeTableRows[i].trainStopping)
            {
                TableRow trainStop = new TableRow();

                TableCell trainStopStation = new TableCell();
                TableCell trainStopTrackNumber = new TableCell();
                TableCell trainStopType = new TableCell(); // Departure or arrival
                TableCell trainStopTime = new TableCell();

                trainStopStation.Text = resultTrain.timeTableRows[i].stationShortCode;
                trainStopTrackNumber.Text = resultTrain.timeTableRows[i].commercialTrack;
                trainStopTime.Text = resultTrain.timeTableRows[i].scheduledTime.ToShortTimeString();
                // Check if the train is arriving or departuring:
                trainStopType.Text = (resultTrain.timeTableRows[i].type == "ARRIVAL") ? "Saapuu" : "Lähtee";

                // Add the cells in a row
                trainStop.Cells.Add(trainStopStation);
                trainStop.Cells.Add(trainStopTrackNumber);
                trainStop.Cells.Add(trainStopType);
                trainStop.Cells.Add(trainStopTime);

                tableTrainResults.Rows.Add(trainStop);
            }
        }
    }

    private List<TrainModel> searchTrainsByStation(string stationName)
    {
        TrafficLocationModel resultStation = trafficLocationService.SelectByStationName(stationName);
        Debug.WriteLine("AKVR:junat.aspx.cs:searchTrainsByStation() - resulting station name: " + resultStation.stationName);
        var resultShorCode = resultStation.stationShortCode;
        Debug.WriteLine("AKVR:junat.aspx.cs:searchTrainsByStation() - resulting station shortcode: " + resultStation.stationShortCode);

        // Can still return empty model, checked later
        return trainService.SelectByStationShortCode(resultShorCode);
    }

    protected void dlTrains_SelectedIndexChanged(object sender, EventArgs e)
    {
        var list = (List<TrainModel>)Session["resultTrainList"];
        Debug.WriteLine("AKVR:junat.aspx.cs:dlTrains_SelectedIndexChanged() - Session['resultTrainList'][selectedIndex].FullTrainName " + list[dlTrains.SelectedIndex].FullTrainName);
        updateTrainInfo((TrainModel)list[dlTrains.SelectedIndex]);
    }
}