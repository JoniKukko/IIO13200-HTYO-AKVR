using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AKVR.Services;
using System.Globalization;

public partial class Controllers_tilastot2_tilastot2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void buttonSelectCauses_Click(object sender, EventArgs e)
    {

        var fromDate = DateTime.Parse(datebox_date.Text);

        var trainService = ServiceFactory.Train();
        var trainList = trainService.SelectCausesByDate(fromDate);

        var stationService = ServiceFactory.TrafficLocation();
        var stations = stationService.SelectAll().ToDictionary(x => x.stationShortCode, x => x.stationName);

        var reasonCategoryService = ServiceFactory.ReasonCategory();
        var reasonCategorys = reasonCategoryService.SelectAll().ToDictionary(x => x.categoryCode, x => x.categoryName);

        var reasonCodeService = ServiceFactory.ReasonCode();
        var reasonCodes = reasonCodeService.SelectAll().ToDictionary(x => x.detailedCategoryCode, x => x.detailedCategoryName);

        
        foreach (var train in trainList)
        {
            foreach (var row in train.timeTableRows)
            {
                TableRow newRow = new TableRow();
                TableCell[] newCells = new TableCell[] {
                    new TableCell(),
                    new TableCell(),
                    new TableCell(),
                    new TableCell()
                };


                newCells[0].Text = train.FullTrainName;
                newCells[1].Text = train.trainCategory;
                newCells[2].Text = stations[row.stationShortCode];

                foreach (var cause in row.causes)
                {
                    string categoryName;
                    string detailedCategoryName;

                    reasonCategorys.TryGetValue(cause.categoryCode, out categoryName);
                    reasonCodes.TryGetValue(cause.detailedCategoryCode != null ? cause.detailedCategoryCode : "" , out detailedCategoryName);

                    newCells[3].Text += categoryName + " - " + detailedCategoryName + "<br>";
                }

                newRow.Cells.AddRange(newCells);
                table_trainData.Rows.Add(newRow);
            }
        }
        

    }
}