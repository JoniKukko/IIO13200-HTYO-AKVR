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
    Dictionary<string, string> stationNames;


    protected void Page_Load(object sender, EventArgs e)
    {
        stationNames = trafficLocationService.SelectAll().ToDictionary(x => x.stationShortCode, x => x.stationName);

        // If URL parameter is given, start search
        if (Request.QueryString["query"] != null && !Page.IsPostBack)
        {
            searchTrains(Request.QueryString["query"]);
            Debug.WriteLine("HAKUSANA " + Request.QueryString["query"]);
        }

        
    }


    protected void btnSearchTrains_Click(object sender, EventArgs e)
    {
        searchTrains(tbSearchTrains.Text);
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
    private void searchTrains(string query)
    {
        // Clear label and dropdown list
        labelTrain.Text = "";
        dlTrains.Items.Clear();

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
                labelTrain.Text = resultTrain.FullTrainName;
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
            resultTrainList = searchTrainsByStation(UppercaseFirst(query));
            Session["resultTrainList"] = resultTrainList;
            Debug.WriteLine("AKVR:junat.aspx.cs:searchTrains() - " + resultTrainList.Count);

            // Check that there is at least one not empty train in the list
            if (resultTrainList.Count != 0 && resultTrainList[0].FullTrainName != "0")
            {
                // Display resulting trains in dropdown list
                dlTrains.DataSource = resultTrainList;
                dlTrains.DataValueField = "FullTrainName";
                dlTrains.DataBind();

                var list = (List<TrainModel>)Session["resultTrainList"];
                updateTrainInfo((TrainModel)list[dlTrains.SelectedIndex]);
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

        labelTrain.Text = "";

        // Timetable
        for (int i = 0; i < resultTrain.timeTableRows.Count() - 1; i++)
        {
            // If the train stops at station
            if (resultTrain.timeTableRows[i].trainStopping)
            {
                TableRow trainStop = new TableRow();

                TableCell trainStopStation = new TableCell();
                TableCell trainStopTrackNumber = new TableCell();
                TableCell trainArrivalTime = new TableCell();
                TableCell trainDepartureTime = new TableCell();

                string stationName;

                // Check that row is not last
                if (i < resultTrain.timeTableRows.Count())
                {
                    // Links to next row or is first row
                    if (resultTrain.timeTableRows[i].type == "ARRIVAL" || i == 0)
                    {

                        // STATION NAME
                        try
                        {
                            stationNames.TryGetValue(resultTrain.timeTableRows[i].stationShortCode, out stationName);
                            trainStopStation.Text = "<a href='asemat?query=" + stationName + "'>" + stationName;
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("AKVR - updateTrainInfo - stationNames.TryGetValue fails: " + ex.Message);
                        }
                        
                        // TRACK NUMBER
                        trainStopTrackNumber.Text = resultTrain.timeTableRows[i].commercialTrack;

                        // ARRIVAL TIME
                        trainArrivalTime.Text = (i != 0)
                            ? resultTrain.timeTableRows[i].scheduledTime.ToShortTimeString()
                            : "";

                        // DEPARTURE TIME
                        trainDepartureTime.Text = (i != 0) 
                            ? resultTrain.timeTableRows[i+1].scheduledTime.ToShortTimeString() 
                            : resultTrain.timeTableRows[i].scheduledTime.ToShortTimeString();

                        // Add the cells in a row
                        trainStop.Cells.Add(trainStopStation);
                        trainStop.Cells.Add(trainStopTrackNumber);
                        trainStop.Cells.Add(trainArrivalTime);
                        trainStop.Cells.Add(trainDepartureTime);

                        // Add row to table
                        tableTrainResults.Rows.Add(trainStop);
                    }
                }
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

    static string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }

        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }
}