using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRT_z_csv
{
    class Analyzer
    {
        private Form1 OperatorPanel;
        public Analyzer(Form1 form)
        {
            OperatorPanel = form;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////Properties////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //do wczytania z pliku config (mozna uwzglednic rodziny)///////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private int ProcentOfFail = (int)(100 / 1); //procent spadow ktory ma znaczenie
        private int ProcentOfRepair = (int)(100 / 1); //procent napraw ktore maja znaczenie
        private int MinOfFail = 1; //minimalna ilosc spadow ktore maja znaczenie
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        struct Repair
        {
            public string repair { get; set; }
            public int counts { get; set; }
        }
        struct Analyze
        {
            public string Equipment { get; set; }
            public string FailureLabel { get; set; }
            public int Counts { get; set; }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////Funkcja glowna/////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Analiza wyników 
        /// </summary>
        /// <param name="list">Lista do analizy</param>
        /// <param name="failList">Lista błędów które nas interesują</param>
        public void startAnalyse(List<Results> list, ListBox failList)
        {
            List<Analyze> failPerEq = new List<Analyze>(); //(Equipment, FailureLabel, Counts)
            List<Analyze> countOfFail = new List<Analyze>(); //(FailureLabel, Counts)
            countOfFail = listOfFail(list, failList, false); //ilosc faili (posortowane wg ilosci)
            failPerEq = listOfFail(list, failList); //ilosc faili wzgledem equipmentow (posortowane wg ilosci)
            checkWhichFailIsValuable(ref countOfFail, ref failPerEq); //zwraca listy z bledami dla ktorych ilosc spadow jest wieksza niz 12.5% wszystkich błedów
            Results result = new Results();
            string[] uniqueDefectLocation = result.UniqueDefectLocation(list);
            string[] uniqueDefectText = result.UniqueDefectText(list);
            repairForFail(list, countOfFail, uniqueDefectLocation, uniqueDefectText); //sprawdzic jakie sa naprawy dla bledow
            //jezeli jakas naprawa wystepuje czesto to sprawdzic na jakiej pozycji sa bledy
            //sprawdzic czy retest pomaga i czy sa jakies naprawy dla tych bledow
            //dla bledow ktore maja niepotwierdzone bledy sprwdzic czy
            //-jakies eq generuje wiecej spadow
            //-sa duplikaty sn
            //-
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////Funkcje dodatkowe//////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Zwraca listę faili posortowanych wg ilości. Jezeli perEq jest true to mamy informacje o eq w danej liscie
        /// </summary>
        /// <param name="list"></param>
        /// <param name="failList"></param>
        /// <param name="perEq">True - info o eq, False - tylko ilosc blędów</param>
        /// <returns></returns>
        private List<Analyze> listOfFail(List<Results> list, ListBox failList, bool perEq = true)
        {
            var resultClass = new Results();
            var equipmentList = resultClass.UniqueEquipmentList(list);
            var tempList = new List<Analyze>();
            if (perEq)
            {
                foreach (var equipment in equipmentList)
                {
                    foreach (var fail in failList.Items)
                    {
                        var temp = list.Where(x => x.FailureLabel == fail.ToString() && x.Equipment == equipment).ToList();
                        int count = temp.Count();
                        if (count > 0)
                        {
                            tempList.Add(new Analyze() { Equipment = equipment.ToString(), FailureLabel = fail.ToString(), Counts = count });
                        }
                    }
                }
            }
            else
            {
                foreach (var fail in failList.Items)
                {
                    var temp = list.Where(x => x.FailureLabel == fail.ToString()).ToList();
                    int count = temp.Count();
                    if (count > 0)
                    {
                        tempList.Add(new Analyze() { FailureLabel = fail.ToString(), Counts = count });
                    }
                }
            }
            tempList = tempList.OrderByDescending(x => x.Counts).ToList();
            return tempList;
        }
        /// <summary>
        /// Sprawdza które błędy są wartościowe i zwraca listę tych ważnych
        /// </summary>
        /// <param name="CountOfFail"></param>
        /// <param name="FailPerEq"></param>
        private void checkWhichFailIsValuable(ref List<Analyze> CountOfFail, ref List<Analyze> FailPerEq)
        {
            int countOfImportantFail = 0;
            foreach (var item in CountOfFail)
            {
                countOfImportantFail += item.Counts; //ilosc wszystkich bledow
            }
            countOfImportantFail = countOfImportantFail / ProcentOfFail; //uwzgledniamy bledy ktorych bedzie wiecej niz 12.5%
            CountOfFail = CountOfFail.Where(x => x.Counts >= countOfImportantFail && x.Counts >= MinOfFail).ToList();
            var tempList = new List<Analyze>();
            foreach (var item in CountOfFail)
            {
                tempList = tempList.Concat(FailPerEq.Where(x => x.FailureLabel == item.FailureLabel).ToList()).ToList();
            }
            FailPerEq = tempList;
        }
        /// <summary>
        /// Zlicza i ocenia wartość napraw. Ocenia czy problem był związany z awarią testera czy jednak produkt był wadliwy.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="CountOfFail"></param>
        /// <param name="uniqueDefectLocation"></param>
        /// <param name="uniqueDefectText"></param>
        private void repairForFail(List<Results> list, List<Analyze> CountOfFail, string[] uniqueDefectLocation, string[] uniqueDefectText) //zliczyc naprawy
        {
            foreach (var failItem in CountOfFail)
            {
                string message = failItem.FailureLabel + " = " + failItem.Counts;
                OperatorPanel.report(message);
                var tempFailList = list.Where(x => x.FailureLabel == failItem.FailureLabel).ToList(); //lista (wszystkie info) z jednym bledem
                foreach (var itemRepair in uniqueDefectText)
                {
                    var repairList = tempFailList.Where(x => x.DefectText == itemRepair).ToList(); //lista (wszystkie info) z jedna naprawa
                    var defectLocationList = new List<Results>();
                        try
                        {
                            defectLocationList = repairList.Where(x => x.DefectLocation != "PCB" && x.DefectLocation != "PCB_1").ToList(); //lista w ktorych wymieniony zostal komponent na plycie wiec produkt byl uszkodzony
                        }
                        catch { }
                        try
                        {
                            repairList = repairList.Where(x => x.DefectLocation == "PCB" || x.DefectLocation == "PCB_1").ToList(); //pomijamy bledy dla ktorych wymieniany jest komponent z winy testera
                        }
                        catch { }
                        if (defectLocationList.Count() >= 1) 
                        {
                        if (itemRepair == "")
                        {
                            message = "W trakcie analizy: " + (defectLocationList.Count() + repairList.Count());
                            OperatorPanel.report(message);
                        }
                        else
                        {
                            message = "\t" + itemRepair + " = " + (defectLocationList.Count() + repairList.Count()) + ". Wymieniono nastepujące komponenty:";
                            OperatorPanel.report(message);
                            message = "\t Problem z produktem: ";
                            OperatorPanel.report(message);
                            foreach (var defectLocation in uniqueDefectLocation) //dla kazdego wymienionego komponentu
                            {
                                int quantityOfReplaced = defectLocationList.Where(x => x.DefectLocation == defectLocation).Count();
                                if (quantityOfReplaced > 0)
                                {
                                    OperatorPanel.report("\t\t\t" + defectLocation + " " + quantityOfReplaced);
                                }
                            }//end foreach component
                            if (repairList.Count() > 0)
                            {
                            message = "\t Problemy z testerem:";
                            OperatorPanel.report(message);
                            foreach (var defectLocation in uniqueDefectLocation) //dla kazdego wymienionego komponentu
                            {
                                int quantityOfReplaced = repairList.Where(x => x.DefectLocation == defectLocation).Count();
                                if (quantityOfReplaced > 0)
                                {
                                    OperatorPanel.report("\t\t" + defectLocation + " " + quantityOfReplaced);
                                }
                            }//end foreach component
                        }
                        }

                        }//end if(defectLocationList.Count() >= 1) 
                }
            }//end forech wg bledow
        }//end of fcn 
    }
}
