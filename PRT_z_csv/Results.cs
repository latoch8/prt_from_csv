using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRT_z_csv
{
    class Results
    {
        public string Family { get; set; }
        public string SerialNumber { get; set; }
        public string StepText { get; set; }
        public string Equipment { get; set; }
        public string StartDataTime { get; set; }
        public string FailureLabel { get; set; }
        public string Message { get; set; }
        public string DefectText { get; set; }
        public string DefectLocation { get; set; }

        private struct Unique
        {
            public string uniqueItem { get; set; }
        }

        public struct Analyze
        {
            public string FailureLabel { get; set; }
            public int Counts { get; set; }
        }

        /* Funkcje do wycinania mniejszych list
         * 
         */
        public List<Results> OneFamilyList(List<Results> list, string familyName) //zwraca tablice dla jednej rodziny
        {
            return list.Where(x => x.Family == familyName).ToList(); ; 
        }

        public List<Results> OneSteptextList (List<Results> list, CheckedListBox stepTextList)
        {
            List<Results> onlyOneStepText = new List<Results>();
            foreach (var item in stepTextList.CheckedItems)
            {
                var temp = list.Where(x => x.StepText == item.ToString()).ToList();
                onlyOneStepText = onlyOneStepText.Concat(temp).ToList();
            }
            return onlyOneStepText; //zwraca tablice z zaznaczonymi opcjami StepText
        }

        public List<Analyze> TopFails(List<Results> list, ListBox failList) //wylicza ilosc bledow i sortuje od najwiekszej ilosci (docelowo tylko top5, wstepnie wszystko)
        {
            var topList = new List<Analyze>();
            foreach (var item in failList.Items)
            {
                var temp = list.Where(x => x.FailureLabel == item.ToString()).ToList();
                topList.Add(new Analyze() { FailureLabel = item.ToString(), Counts = temp.Count() });
            }
            var sorted = topList.OrderByDescending(x => x.Counts).ToList();
            return sorted;
        }

        public List<Results> OnlyOneFail(List<Results> list, string fail) //zwraca tablice z jednym bledem do wyswietlenia
        {
            return list.Where(x => x.FailureLabel == fail).ToList();
        }

        /* Funkcje zwracajace unikalne wartosci dla poszczegolnych tablic
         * 
         */
        public string[] UniqueFamilyList(List<Results> list) //zwraca wszystkie rodziny z listy
        {
            var unique = new List<Unique>();
            foreach (var item in list)
            {
                unique.Add(new Unique() { uniqueItem = item.Family });
            }
            return UniqueFcn(unique);
        }

        public string[] UniqueSteptextList(List<Results> list) //zwraca equipmenty dla danej rodziny
        {
            var unique = new List<Unique>();
            foreach (var item in list)
            {
                unique.Add(new Unique() { uniqueItem = item.StepText });
            }
            return UniqueFcn(unique);
        }

        public string[] UniqueFailList(List<Results> list) //zwraca faile dla danego equipmentu
        {
            var unique = new List<Unique>();
            foreach (var item in list)
            {
                unique.Add(new Unique() { uniqueItem = item.FailureLabel });
            }
            return UniqueFcn(unique);
        }

        public string[] UniqueDefectLocation(List<Results> list)
        {
            var unique = new List<Unique>();
            foreach (var item in list)
            {
                unique.Add(new Unique() { uniqueItem = item.DefectLocation });
            }
            return UniqueFcn(unique);
        }

        public string[] UniqueDefectText(List<Results> list)
        {
            var unique = new List<Unique>();
            foreach (var item in list)
            {
                unique.Add(new Unique() { uniqueItem = item.DefectText });
            }
            return UniqueFcn(unique);
        }

        private string[] UniqueFcn(List<Unique> unique) //wycina unikaty z listy i przepisuje do tablicy
        {
            string[] temp = new string[1000]; //tymczasowo za duza tablica
            unique = unique.OrderBy(x => x.uniqueItem).ToList();
            unique = unique.Union(unique).ToList();
            int i = 0;
            foreach (var item in unique)
            {
                temp[i] = item.uniqueItem;
                i++;
            }
            Array.Resize(ref temp, i); //dopasowanie tablicy do ilosci elementow
            return temp;
        }

        public string[] UniqueEquipmentList(List<Results> list)
        {
            var unique = new List<Unique>();
            foreach (var item in list)
            {
                unique.Add(new Unique() { uniqueItem = item.Equipment });
            }
            return UniqueFcn(unique);
        }
    }
}
