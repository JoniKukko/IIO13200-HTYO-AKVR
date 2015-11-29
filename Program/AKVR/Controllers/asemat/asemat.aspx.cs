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
    Dictionary<string, string> stationNames;

    protected void Page_Load(object sender, EventArgs e)
    {
        tbStationDate.Attributes["placeholder"] = DateTime.Now.ToShortDateString();
        tbStationTime.Attributes["placeholder"] = DateTime.Now.ToShortTimeString();

        stationNames = trafficLocationService.SelectAll().ToDictionary(x => x.stationShortCode, x => x.stationName);

        // If URL parameter is given, start search
        if (Request.QueryString["query"] != null && !Page.IsPostBack)
        {
            searchStations(Request.QueryString["query"], tbStationDate.Text, tbStationTime.Text);
        }
    }

    protected void btnSearchStations_Click(object sender, EventArgs e)
    {
        searchStations(tbSearchStations.Text, tbStationDate.Text, tbStationTime.Text);
    }


    private string parseDateAndTimeString(string date, string time)
    {
        string parsedDateAndTime = DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss");
        Debug.WriteLine("AKVR datetimestring Now()- " + parsedDateAndTime);

        try
        {
            DateTime dt = Convert.ToDateTime(date + " " + time);
            parsedDateAndTime = dt.ToString();
            Debug.WriteLine("AKVR datetimestring parsed - " + parsedDateAndTime);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("AKVR datetime not parsed - " + ex.Message);
            labelStation.Text = "Anna päivämäärä ja aika muodossa pp.kk.vvvv hh.mm";
        }

        return parsedDateAndTime;
    }


    private void searchStations(string query, string date, string time)
    {
        Debug.WriteLine("AKVR - query = " + query);

        string timeQueryString = DateTime.Now.ToString();

        // Check that both date and time fields have something in them
        if (!(string.IsNullOrWhiteSpace(tbStationDate.Text) || string.IsNullOrWhiteSpace(tbStationTime.Text)))
        {
            labelStation.Text = "";
            timeQueryString = parseDateAndTimeString(date, time);
        }
        else
        {
            labelStation.Text = "";
        }

        Session["StationShortcode"] = getShortcodeByStation(query);
        Debug.WriteLine("AKVR.asemat.aspx.cs:searchStations() - shortcode: " + (string)Session["StationShortcode"]);

        List<TrainModel> resultTrainList;

        if ((string)Session["StationShortcode"] != "")
        {
            Debug.WriteLine("AKVR datetimestring when sending - " + timeQueryString);
            // Tällä sitten jos vielä tekis jotain
            resultTrainList = trainService.SelectByStationShortCode((string)Session["StationShortcode"], timeQueryString);

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

                    if (row.stationShortCode == (string)Session["StationShortcode"])
                    {
                        TableRow trainStop = new TableRow();

                        TableCell trainStopDate = new TableCell();
                        TableCell trainStopDestination = new TableCell();
                        TableCell trainStopTrainNumber = new TableCell();
                        TableCell trainStopTrackNumber = new TableCell();
                        TableCell trainStopTime = new TableCell();
                        TableCell trainStopEstimate = new TableCell();

                        trainStopDate.Text = row.scheduledTime.ToShortDateString();

                        // Trains final destination
                        //trainStopDestination.Text = resultTrain.timeTableRows.Last<Timetable>().stationShortCode;
                        var station = stationNames[resultTrain.timeTableRows.Last<Timetable>().stationShortCode];
                        trainStopDestination.Text = "<a href='asemat?query=" +
                                                    station +
                                                    "'>" +
                                                    station;

                        trainStopTrainNumber.Text = "<a href='junat?query=" + 
                                                    resultTrain.trainNumber + 
                                                    "'>" + 
                                                    resultTrain.FullTrainName + 
                                                    "</a>";
                        trainStopTrackNumber.Text = row.commercialTrack;
                        trainStopTime.Text = row.scheduledTime.ToShortTimeString();

                        // Added only if there is a meaningful value
                        // Sometimes is the same as the scheduled time
                        if (row.liveEstimateTime.ToShortTimeString() != "0:00" && row.liveEstimateTime.ToShortTimeString() != "0.00")
                        {
                            trainStopEstimate.Text = row.liveEstimateTime.ToShortTimeString();
                        }

                        // Add the cells in a row
                        trainStop.Cells.Add(trainStopDate);
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
        }
    }

    private string getShortcodeByStation(string stationName)
    {
        TrafficLocationModel resultStation = trafficLocationService.SelectByStationName(UppercaseFirst(stationName));
        Debug.WriteLine("AKVR:asemat.aspx.cs:searchTrainsByStation() - resulting station name: " + resultStation.stationName);
        return resultStation.stationShortCode;
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
