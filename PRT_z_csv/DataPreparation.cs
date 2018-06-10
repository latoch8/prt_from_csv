using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PRT_z_csv
{
    class DataPreparation
    {
        Results Data;
        List<Results> List = new List<Results>();

        public List<Results> LoadDataFromFile(Results classData)
        {
            Data = classData;
            OpenFileDialog csvFile = new OpenFileDialog(); 
            csvFile.Filter = "Fail per Test Data|*.csv";
            csvFile.Title = "Wczytaj wyniki!";
            if (csvFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ParsData(csvFile); //obrobka pliku
            }
            return List;
        }
        string[] TabData;
        private void ParsData(OpenFileDialog csvFile)
        {
            //adresy kolumn w których znajują się zmienne (bardziej uniwersalne dane wejsciowe muszą miec tylko poprawnie nazwane kolumny)
            int addrFamily = -1, addrSerialNumber = -1, addrDefectLocation = -1, addrDefectText = -1, 
                addrEquipment = -1, addrFailureLabel = -1, addrMessage = -1, addrStartDataTime = -1, addrStepText = -1;
            StreamReader csvResults = new StreamReader(csvFile.OpenFile());
            string line;
            while ((line = csvResults.ReadLine()) != null)
            {
                /*
                 *komentarz message zawiera , wiec nie mozna uzyc string.split(',')
                 * trzeba odnaleźć message i skleic w jedno
                */
                TabData = SpecialStringSplit(line); 
                if (addrFamily == -1) //automatyczne wyznaczenie kolumn w których znajdją się odpowiednie zmienne
                {
                    int actualColumn = 0;
                    foreach (var item in TabData)
                    {
                        switch (item)
                        {
                            case "Family": addrFamily = actualColumn; break;
                            case "SerialNumber": addrSerialNumber = actualColumn; break;
                            case "StepText": addrStepText = actualColumn; break;
                            case "Equipment": addrEquipment = actualColumn; break;
                            case "StartDateTime": addrStartDataTime = actualColumn; break;
                            case "FailureLabel": addrFailureLabel = actualColumn; break;
                            case "Message": addrMessage = actualColumn; break;
                            case "DefectText": addrDefectText = actualColumn; break;
                            case "DefectLocation": addrDefectLocation = actualColumn; break;
                        }
                        actualColumn++;
                    }
                }
                else 
                {
                    try //niektóre linie są puste lub zawierają pojedyncze wpisy ktore nas nie dotycza i nie da sie wpisac takich linii dlatego try
                    {
                        Add(TabReader(addrFamily), TabReader(addrSerialNumber), TabReader(addrDefectLocation),TabReader(addrDefectText), TabReader(addrEquipment),
                            TabReader(addrFailureLabel), TabReader(addrMessage), TabReader(addrStartDataTime), TabReader(addrStepText));
                    }
                    catch { }
                }
            }
            csvResults.Close();
        }

        private string TabReader(int addr) //pomija braki w tabelach wejsciowych
        {
            return addr != -1 ? TabData[addr] : "";
        }

        private void Add(string family, string serialNumber, string defectLocation, string defectText, string equipment,
                 string failureLabel, string message, string startDataTime, string stepText)
        {
            List.Add(new Results()
            {
                Family = family,
                SerialNumber = serialNumber,
                DefectLocation = defectLocation,
                DefectText = defectText,
                Equipment = equipment,
                FailureLabel = failureLabel,
                Message = message,
                StartDataTime = startDataTime,
                StepText = stepText
            });
        }

        private string[] SpecialStringSplit(string stringToSplit) //dzielenie stringa na poszczegolne kolumny z uwzglednieniem ze wiadomosc zawiera w sobie przecinki 
        {
            string[] TabData = stringToSplit.Split(',');
            int i = 0, startMerge = 0, countOfMerge = 0;
            bool startFound = false;

            foreach (var item in TabData)
            {
                if (item.Length > 0)
                {
                    if (startFound) //laczenie zle podzielonej wiadomosci
                    {
                        TabData[startMerge] += ", " + item;
                        countOfMerge++;
                        if (item[item.Length-1] == '"') //szukanie konca wiadomosci
                        {
                            break;
                        }
                    }
                    else if (item[0] == '"') //szukanie poczatku wiadomosci
                    {
                        startMerge = i;
                        startFound = true;
                    }
                    i++;
                }
            }
            if (startFound)
            {
                for (i = 1; i < (TabData.Length - startMerge - countOfMerge); i++)
                {
                    TabData[startMerge + i] = TabData[startMerge + countOfMerge + i];
                }
                Array.Resize<string>(ref TabData, (TabData.Length - countOfMerge));
            }        
            return TabData;
        }
    }
}
