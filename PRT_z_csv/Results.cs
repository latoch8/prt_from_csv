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
        /// <summary>
        /// Zwraca tablice dla jednej rodziny
        /// </summary>
        /// <param name="list">Lista z której sortuje</param>
        /// <param name="familyName">Rodzina po której sortuje</param>
        /// <returns></returns>
        public List<Results> OneFamilyList(List<Results> list, string familyName) 
        {
            return list.Where(x => x.Family == familyName).ToList(); ; 
        }
        /// <summary>
        /// Zwraca tablice z zaznaczonymi opcjami StepText
        /// </summary>
        /// <param name="list"></param>
        /// <param name="stepTextList"></param>
        /// <returns></returns>
        public List<Results> OneSteptextList (List<Results> list, CheckedListBox stepTextList)
        {
            List<Results> onlyOneStepText = new List<Results>();
            foreach (var item in stepTextList.CheckedItems)
            {
                var temp = list.Where(x => x.StepText == item.ToString()).ToList();
                onlyOneStepText = onlyOneStepText.Concat(temp).ToList();
            }
            return onlyOneStepText; 
        }
        /// <summary>
        /// Wylicza ilosc bledow i sortuje od najwiekszej ilosci (docelowo tylko top5, wstepnie wszystko)
        /// </summary>
        /// <param name="list">Lista z której wyciągamy błędy</param>
        /// <param name="failList">Lista błędów które nas interesują</param>
        /// <returns></returns>
        public List<Analyze> TopFails(List<Results> list, ListBox failList)
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
        /// <summary>
        /// Zwraca tablice z jednym bledem do wyswietlenia
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fail"></param>
        /// <returns></returns>
        public List<Results> OnlyOneFail(List<Results> list, string fail)
        {
            return list.Where(x => x.FailureLabel == fail).ToList();
        }
        /// <summary>
        /// Zwraca wszystkie rodziny z listy
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string[] UniqueFamilyList(List<Results> list)
        {
            var unique = new List<Unique>();
            foreach (var item in list)
            {
                unique.Add(new Unique() { uniqueItem = item.Family });
            }
            return UniqueFcn(unique);
        }
        /// <summary>
        /// Zwraca equipmenty dla danej rodziny
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string[] UniqueSteptextList(List<Results> list) 
        {
            var unique = new List<Unique>();
            foreach (var item in list)
            {
                unique.Add(new Unique() { uniqueItem = item.StepText });
            }
            return UniqueFcn(unique);
        }
        /// <summary>
        /// Zwraca faile dla danego equipmentu
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string[] UniqueFailList(List<Results> list) 
        {
            var unique = new List<Unique>();
            foreach (var item in list)
            {
                unique.Add(new Unique() { uniqueItem = item.FailureLabel });
            }
            return UniqueFcn(unique);
        }
        /// <summary>
        /// Zwraca Liste pozycji na których jest błąd
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string[] UniqueDefectLocation(List<Results> list)
        {
            var unique = new List<Unique>();
            foreach (var item in list)
            {
                unique.Add(new Unique() { uniqueItem = item.DefectLocation });
            }
            return UniqueFcn(unique);
        }
        /// <summary>
        /// Zwraca listę analiz naprawiacza
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string[] UniqueDefectText(List<Results> list)
        {
            var unique = new List<Unique>();
            foreach (var item in list)
            {
                unique.Add(new Unique() { uniqueItem = item.DefectText });
            }
            return UniqueFcn(unique);
        }
        /// <summary>
        /// Zwraca unikalne wartości dla listy jednowymiarowej
        /// </summary>
        /// <param name="unique"></param>
        /// <returns></returns>
        private string[] UniqueFcn(List<Unique> unique) 
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
        /// <summary>
        /// Zwraca liste equipmentow
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
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
