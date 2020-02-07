using System;
using System.Text.RegularExpressions;


// validate user input
public class Validate
{
    public string cleanInput { get; set; }

    public Validate() {
        cleanInput = "";
    }
    public string displayuserMsg()
    {
        string message = "Please Enter Code or Procedure";
        return message;
    }

    public string errorMsg1()
    {
        string message = "Please enter a code";
        return message;
    }

    public string errorMsg2()
    {
        string message = "Input is too long";
        return message;
    }

    public string successMsg()
    {
        string message = "Message is validated";
        return message;
    }

    // clean special characters
    public bool CleanUserInput(string UncleanInput)
    {
        // using regex to remove any special characters
        try
        {
             cleanInput = Regex.Replace(UncleanInput, @"[^\w.,@\%\s]", "",
                                 RegexOptions.None, TimeSpan.FromSeconds(1.5));
            return true;
        }
        // If we timeout when replacing invalid characters, 
        // we should return Empty.
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    // checks the input is empty or not and return clean inputs
    public bool validateCode(string input)
    {
        //validates input

        //clean userinput of any special characters
        CleanUserInput(input);

        //input cannot be empty
        if (String.IsNullOrEmpty(input))
        {
            errorMsg1();
            return false;
        }
        //input should be of reasonal length
        else if (input.Length > 70)
        {
            errorMsg2();
            return false;
        }
        else //Success state, input is valid.
        {
            successMsg();

            return true;
        }
    }

}