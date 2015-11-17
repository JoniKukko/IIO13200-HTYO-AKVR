using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controllers_junat_junat : System.Web.UI.Page
{
    AKVR.Services.Train.TrainService trainService = AKVR.Services.ServiceFactory.Train();

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btnSearchTrains_Click(object sender, EventArgs e)
    {
        searchTrains();
    }

    private void searchTrains()
    {
        var query = tbSearchTrains.Text;

        // Checks if 1-3 numbers, train number
        Regex regexTrainNumber = new Regex(@"^[0-9]{1,3}$");
        Match check = regexTrainNumber.Match(query);

        // Determine the type of the query
        // check.Success == true when the query is 1-3 numbers, train number
        // Otherwise expect string
        if (check.Success)
        {
            // Query from text to int
            int intQuery;
            Int32.TryParse(query, out intQuery);

            searchTrainByNumber(intQuery);
        }
        else
        {

        }

        
    }

    private void searchTrainByNumber(int trainNumber)
    {
        // Search the train and make it an object
        AKVR.Services.Train.TrainModel resultTrain = trainService.SelectByTrainNumber(trainNumber);

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

        /**
        // Make necessary table elements for train info
        TableRow trainRow = new TableRow();
        TableCell resultTrainType = new TableCell();
        TableCell resultTrainNumber = new TableCell();

        resultTrainType.Text = resultTrain.trainType;
        resultTrainNumber.Text = resultTrain.trainNumber.ToString();

        trainRow.Cells.Add(resultTrainType);
        trainRow.Cells.Add(resultTrainNumber);

        tableTrainResults.Rows.Add(trainRow);
        */
    }

    private void searchTrainsByStation(string stationName)
    {

    }
}