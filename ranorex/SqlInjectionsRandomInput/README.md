SqlInjectionsRandomInput
Overview

SqlInjectionsRandomInput is a Ranorex UserCode method designed to test Free Text Search fields (or any other text input field) for potential SQL Injection and Search Query Injection vulnerabilities.

The method automatically selects a random payload from a predefined CSV file and stores it in a Ranorex variable. The generated payload can then be used within your test case to validate how the application handles potentially malicious input.

How It Works
A payload is randomly selected from a CSV file containing SQL Injection and Search Query Injection test strings.

The selected payload is stored in the Ranorex variable:

SQL_Injections_Test_Input

The payload is entered into the target input field.
The search is executed (e.g., by clicking the Search icon and/or pressing Enter).
The application response is validated.

The validation verifies whether:

the entered payload remains unchanged in the input field (expected behavior), or
additional, modified, or unexpected characters appear (potential issue), which could indicate:
improper input handling,
information disclosure,
security weaknesses,
possible SQL/Search Query Injection vulnerabilities.

Payload Source

Payloads are loaded from a CSV file:
string filePath = @"<insert path to payload file here>\SQL_Search_Test_Payloads_Runtime.csv";
The payload list:

is maintained centrally in a single CSV file,
is randomly accessed during test execution,
can be extended at any time without modifying the test logic,
supports adding new attack patterns and edge cases easily.

The random selection logic is implemented in the associated Ranorex UserCode.

Important: Use Set Value Instead of Key Sequence

When inserting payloads into input fields, it is strongly recommended to use Set Value instead of Key Sequence.

Why?

Many SQL Injection payloads contain special characters such as:
{ }
' "
( )
;
--

Ranorex Key Sequence may interpret some of these characters as special keyboard commands, which can lead to:

unexpected behavior,
invalid input,
Ranorex exceptions,
failed test executions.

Using Set Value ensures that the payload is inserted exactly as generated.

Usage
Step 1 – Create a Ranorex Variable

Create a variable named:
SQL_Injections_Test_Input

and make it available within your recording/module.

Step 2 – Execute the UserCode Method

Before using the variable, execute the SqlInjectionsRandomInput UserCode method.

This method generates a random payload and stores it in the variable.

Step 3 – Insert the Payload Using Set Value

Use Set Value to populate the target input field with the generated payload.

$SQL_Injections_Test_Input

Avoid using Key Sequence for this purpose.

Expected Result

A successful test execution should show that:

the application accepts the input safely,
the payload remains unchanged in the field,
no application errors occur,
no unexpected system behavior is triggered.

Any modification of the payload, unexpected application response, exposed data, error messages, or system instability should be investigated as a potential security issue.

Extending the Payload Library

To add new test cases, simply append additional payloads to:

SQL_Search_Test_Payloads_Runtime.csv

No changes to the UserCode implementation are required.

This allows the payload library to grow continuously while keeping the test step implementation unchanged.
