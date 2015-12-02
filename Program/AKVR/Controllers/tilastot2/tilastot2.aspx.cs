using System;
using System.Linq;
using System.Web.UI.WebControls;
using AKVR.Services;

public partial class Controllers_tilastot2_tilastot2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }


    // haetaan annetun päivämäärän mukaan myöhästymisiin syyt
    protected void buttonSelectCauses_Click(object sender, EventArgs e)
    {

        // haetaan päivämäärä input kentästä
        var fromDate = DateTime.Parse(datebox_date.Text);

        // Haetaan päivämäärän syyt
        var trainService = ServiceFactory.Train();
        var trainList = trainService.SelectCausesByDate(fromDate);

        // Haetaan asemien nimet dictionaryyn
        var stationService = ServiceFactory.TrafficLocation();
        var stations = stationService.SelectAll().ToDictionary(x => x.stationShortCode, x => x.stationName);

        // Haetaan syykategoriat ja syykoodit omiin dictionaryihinsä
        var reasonCategoryService = ServiceFactory.ReasonCategory();
        var reasonCategorys = reasonCategoryService.SelectAll().ToDictionary(x => x.categoryCode, x => x.categoryName);
        var reasonCodeService = ServiceFactory.ReasonCode();
        var reasonCodes = reasonCodeService.SelectAll().ToDictionary(x => x.detailedCategoryCode, x => x.detailedCategoryName);

        
        // käydään jokainen juna läpi
        foreach (var train in trainList)
        {
            // käydään jokaisen junan jokainen asema läpi
            foreach (var row in train.timeTableRows)
            {
                // luodaan uusi rivi taulukkoon ja solut sille
                TableRow newRow = new TableRow();
                TableCell[] newCells = new TableCell[] {
                    new TableCell(), new TableCell(), new TableCell(), new TableCell()
                };


                // laitetaan soluihin perustavaraa
                newCells[0].Text = train.FullTrainName;
                newCells[1].Text = train.trainCategory;
                newCells[2].Text = stations[row.stationShortCode];


                // käydään jokaisen junan jokaisen aseman jokainen myöhästymiseen johtanut syy läpi
                foreach (var cause in row.causes)
                {
                    string categoryName, detailedCategoryName;

                    // haetaan syykoodeja vastaavat kokonaiset syyt 
                    reasonCategorys.TryGetValue(cause.categoryCode, out categoryName);
                    reasonCodes.TryGetValue(cause.detailedCategoryCode != null ? cause.detailedCategoryCode : "" , out detailedCategoryName);
                    // ja laitetaan viimeiseen soluun
                    newCells[3].Text += categoryName + " - " + detailedCategoryName + "<br>";
                }

                // lisätään rivi taulukkoon
                newRow.Cells.AddRange(newCells);
                table_trainData.Rows.Add(newRow);
            }
        }


    }
}