using AKVR.Services.TrafficLocation;
using AKVR.Services.Train;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controllers_tilastot_tilastot : System.Web.UI.Page
{
    TrainService trainService = AKVR.Services.ServiceFactory.Train();
    TrafficLocationService trafficLocationService = AKVR.Services.ServiceFactory.TrafficLocation();
    Dictionary<string, string> stationNames;

    protected void Page_Load(object sender, EventArgs e)
    {
        stationNames = trafficLocationService.SelectAll().ToDictionary(x => x.stationShortCode, x => x.stationName);
    }

    protected void btnSearchDelayedTrains_Click(object sender, EventArgs e)
    {
        labelDelay.Text = "";

        DateTime from = DateTime.Now;
        DateTime to = DateTime.Now;

        try
        {
            from = DateTime.ParseExact(tbDelayFrom.Text, "d.MM.yyyy", new CultureInfo("fi-FI"));
            to = DateTime.ParseExact(tbDelayTo.Text, "d.MM.yyyy", new CultureInfo("fi-FI"));

            TimeSpan difference = to - from;

            // To make sure user doesn't give too large difference in days,
            // otherwise it takes zillion hours to complete or crashes
            if (difference.Days <= 5)
            {
                displayDelayedTrains(from, to);
            }
        }
        catch (Exception ex)
        {
            labelDelay.Text = "Anna päivämäärät muodossa pp.kk.vvvv. Päivämäärien ero ei saa olla yli viittä päivää.";
            Debug.WriteLine("AKVR: btnSearchDelayedTrains_Click() - " + ex.Message);
        }

        
    }

    // Get delayed trains by dates
    // Set the datasource of the table
    protected void displayDelayedTrains(DateTime from, DateTime to)
    {
        Debug.WriteLine("AKVR: from - " + from + " to - " + to);
        List<TrainModel> resultTrains = trainService.SelectDelaysBetweenDates(from, to);

        repeaterDelayedTrains.DataSource = resultTrains;
        repeaterDelayedTrains.DataBind();
    }
}