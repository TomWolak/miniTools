/* Required namespaces for Ranorex UserCode

using System;
using System.Collections.Generic;
using System.Text;
using Ranorex;

*/


// -------------------- Methode SQL Inject Random Input ------------------ //
[UserCodeMethod]
public void SqlInjectionsRandomInput()
{
    string filePath =
        @"<insert path to payload file here>\SQL_Search_Test_Payloads_Runtime.csv";

    if (!System.IO.File.Exists(filePath))
    {
        throw new Exception("CSV file was not found: " + filePath);
    }

    List<string[]> activeRows = new List<string[]>();

    string[] lines = System.IO.File.ReadAllLines(filePath, Encoding.UTF8);

    if (lines.Length < 2)
    {
        throw new Exception("CSV file has no data rows: " + filePath);
    }

    // Header:
    // InputId;Payload;Category;Description;Active;ExpectedBehavior
    for (int i = 1; i < lines.Length; i++)
    {
        string line = lines[i];

        if (string.IsNullOrEmpty(line))
        {
            continue;
        }

        string[] columns = line.Split(';');

        if (columns.Length < 6)
        {
            continue;
        }

        string active = columns[4];

        if (active.Equals("true", StringComparison.OrdinalIgnoreCase))
        {
            activeRows.Add(columns);
        }
    }

    if (activeRows.Count == 0)
    {
        throw new Exception("CSV file contains no active payloads.");
    }

    Random random = new Random();
    int selectedRowIndex = random.Next(0, activeRows.Count);

    string[] selectedRow = activeRows[selectedRowIndex];

    string inputId = selectedRow[0];
    string payload = selectedRow[1];
    string category = selectedRow[2];
    string description = selectedRow[3];
    string expectedBehavior = selectedRow[5];

    this.SQL_Injections_Test_Input = payload;

    Report.Info(
        "SQL/Search random input selected. " +
        "InputId: " + inputId +
        ", Category: " + category +
        ", Description: " + description +
        ", ExpectedBehavior: " + expectedBehavior +
        ", Payload: " + this.SQL_Injections_Test_Input
    );
}

// -------------------- Ende der Methode SQL Inject Random Input ------------------ //