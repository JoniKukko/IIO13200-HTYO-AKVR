using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AKVR.Services.Train;
using AKVR.Services.TrafficLocation;

public partial class Controllers_asemat_asemat : System.Web.UI.Page
{
    TrainService trainService = AKVR.Services.ServiceFactory.Train();
    TrafficLocationService trafficLocationService = AKVR.Services.ServiceFactory.TrafficLocation();

    protected void Page_Load(object sender, EventArgs e)
    {
        tbStationDate.Attributes["placeholder"] = DateTime.Now.ToShortDateString();
        tbStationTime.Attributes["placeholder"] = DateTime.Now.ToShortTimeString();
    }

    protected void btnSearchStations_Click(object sender, EventArgs e)
    {
        searchStations();
    }

    private void searchStations()
    {
        DateTime timeQuery = DateTime.Now;
        string timeQueryString = Convert.ToString(timeQuery);
        Debug.WriteLine("AKVR.asemat.aspx.cs:searchStations() - timeQueryString: " + timeQueryString);

        // Check that both date and time fields have something in them
        if (!(string.IsNullOrWhiteSpace(tbStationDate.Text) || string.IsNullOrWhiteSpace(tbStationTime.Text)))
        {
            try
            {
                // If the DateTime is later needed instead of a string
                timeQuery = DateTime.ParseExact(tbStationDate.Text + " " + tbStationTime.Text, "dd.MM.yyyy HH.mm", CultureInfo.InvariantCulture);
                Debug.WriteLine("AKVR.asemat.aspx.cs:searchStations() - timeQuery: " + timeQuery);
                timeQueryString = Convert.ToString(timeQuery);

                // Nollataan label jos aiempi syöte oli virheellinen
                labelStation.Text = "";
            }
            catch (Exception ex)
            {
                Debug.WriteLine("AKVR.asemat.aspx.cs:searchStations() - Time query could not be parsed - " + ex.Message);
                labelStation.Text = "Anna päivämäärä ja aika muodossa: pp.kk.vvvv hh.mm";
            }

        }
        else
        {
            labelStation.Text = "";
        }

        Session["shortcode"] = getShortcodeByStation(tbSearchStations.Text);
        Debug.WriteLine("AKVR.asemat.aspx.cs:searchStations() - shortcode: " + (string)Session["shortcode"]);

        List<TrainModel> resultTrainList;

        if ((string)Session["shortcode"] != "")
        {
            // Tällä sitten jos vielä tekis jotain
            resultTrainList = trainService.SelectByStationShortCode((string)Session["shortcode"], timeQueryString);

            populateTables(resultTrainList);

        }
        else
        {
            labelStation.Text = "Asemia ei löytynyt.";
        }

    }

    private void populateTables(List<TrainModel> resultTrainList)
    {
        foreach (var resultTrain in resultTrainList)
        {
            Debug.WriteLine("AKVR.asemat.aspx.cs:populateTables() - train changed");

            try
            {
                labelStation.Text = "";

                foreach (var row in resultTrain.timeTableRows)
                {

                    if (row.stationShortCode == (string)Session["shortcode"])
                    {
                        TableRow trainStop = new TableRow();

                        TableCell trainStopDestination = new TableCell();
                        TableCell trainStopTrainNumber = new TableCell();
                        TableCell trainStopTrackNumber = new TableCell();
                        TableCell trainStopTime = new TableCell();
                        TableCell trainStopEstimate = new TableCell();


                        // Trains final destination
                        trainStopDestination.Text = resultTrain.timeTableRows.Last<Timetable>().stationShortCode;

                        trainStopTrainNumber.Text = resultTrain.FullTrainName;
                        trainStopTrackNumber.Text = row.commercialTrack;
                        trainStopTime.Text = row.scheduledTime.ToShortTimeString();

                        // Added only if there is a meaningful value
                        // Sometimes is the same as the scheduled time
                        if (row.liveEstimateTime.ToShortTimeString() != "0:00" && row.liveEstimateTime.ToShortTimeString() != "0.00")
                        {
                            trainStopEstimate.Text = row.liveEstimateTime.ToShortTimeString();
                        }

                        // Add the cells in a row
                        trainStop.Cells.Add(trainStopDestination);
                        trainStop.Cells.Add(trainStopTrackNumber);
                        trainStop.Cells.Add(trainStopTime);
                        trainStop.Cells.Add(trainStopEstimate);
                        trainStop.Cells.Add(trainStopTrainNumber);

                        if (row.type == "ARRIVAL")
                        {
                            tableArrivingTrains.Rows.Add(trainStop);
                        }
                        else if (row.type == "DEPARTURE")
                        {
                            tableDepartingTrains.Rows.Add(trainStop);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Fails if returned train is empty, aka no trains found
                labelStation.Text = "Asemia ei löytynyt.";
            }
            /*
            for (int i = 0; i < resultTrain.timeTableRows.Count() - 1; i++)
            {
                // If the train stops
                if (resultTrain.timeTableRows[i].type == "Arrival")
                {
                    trainStopDestination.Text = resultTrain.timeTableRows[i].stationShortCode;
                    trainStopTrackNumber.Text = resultTrain.timeTableRows[i].commercialTrack;
                    trainStopTime.Text = resultTrain.timeTableRows[i].scheduledTime.ToShortTimeString();
                    // Check if the train is arriving or departuring

                    // Add the cells in a row
                    trainStop.Cells.Add(trainStopDestination);
                    trainStop.Cells.Add(trainStopTrackNumber);
                    trainStop.Cells.Add(trainStopTime);

                    tableArrivingTrains.Rows.Add(trainStop);
                }
            }
            */
        }
    }

    private string getShortcodeByStation(string stationName)
    {
        TrafficLocationModel resultStation = trafficLocationService.SelectByStationName(stationName);
        Debug.WriteLine("AKVR:asemat.aspx.cs:searchTrainsByStation() - resulting station name: " + resultStation.stationName);
        return resultStation.stationShortCode;
    }
}
