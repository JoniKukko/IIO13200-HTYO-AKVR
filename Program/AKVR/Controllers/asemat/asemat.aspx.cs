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

// ADD HERE: Parse given date and time to a single string 
// dd.mm.yyyy hh:ii:ss

        /*
        // If the DateTime is later needed instead of a string
        try
        {
            timeQuery = DateTime.ParseExact(tbStationDate.Text + " " + tbStationTime.Text, "dd.MM.yyyy HH.mm", CultureInfo.InvariantCulture);
            Debug.WriteLine("AKVR.asemat.aspx.cs:searchStations() - timeQuery: " + timeQuery);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("AKVR.asemat.aspx.cs:searchStations() - Time query could not be parsed");
        }
        */

        string shortcode = getShortcodeByStation(tbSearchStations.Text);
        Debug.WriteLine("AKVR.asemat.aspx.cs:searchStations() - shortcode: " + shortcode);

        List<TrainModel> resultTrainList;

        if (shortcode != "")
        {
            // Tällä sitten jos vielä tekis jotain
            resultTrainList = trainService.SelectByStationShortCode(shortcode, timeQueryString);
        }
        
    }

    private string getShortcodeByStation(string stationName)
    {
        TrafficLocationModel resultStation = trafficLocationService.SelectByStationName(stationName);
        Debug.WriteLine("AKVR:asemat.aspx.cs:searchTrainsByStation() - resulting station name: " + resultStation.stationName);
        return resultStation.stationShortCode;
    }
}
