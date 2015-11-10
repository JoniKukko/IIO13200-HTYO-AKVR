using System;
using System.Collections.Generic;
using System.Linq;
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
        // Query string to int
        string query = tbSearchTrains.Text;
        int intQuery;
        Int32.TryParse(query, out intQuery);    // Ehkä jotakin virheenkäsittelyä olisi hyvä.

        // If the query matches many trains:
        //List<AKVR.Services.Train> listOfTrains ...

        // If the query matches one train:
        AKVR.Services.Train.TrainModel resultTrain = trainService.SelectByTrainNumber(intQuery);

        TextBox1.Text = resultTrain.trainType;
    }
}