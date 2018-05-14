/*
 * Written by: Nicholas Cockcroft
 * Date: March 20, 2018
 * Course: .NET Environment
 * Assignment: Lab 9
 * 
 * Description: Write a Split tester.  It will be a windows application with 3 text boxes. 
 * In the first two you enter the pattern and the string to be split according to the pattern. 
 * The last text box will be read-only with vertical scrolling which will contain the substrings
 * into which the original string was parsed.  This lab is a great way to test your knowledge of 
 * regular expressions and the Split method by playing with this code.  Please play with your program.  
 * When the user hits enter in any text box, apply the split.
 * 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions; // Regular Expressions
using System.Collections.Generic; // Stack

namespace Lab9
{
    public partial class lab9Form : Form
    {
        public lab9Form()
        {
            InitializeComponent();
        }

        private void patternTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void patternTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // If the key is not enter, return
            if(e.KeyCode != Keys.Enter)
            {
                return;
            }

            // Otherwise, we call a fuction to perform the splitting with the pattern
            splitText();
        }

        private void stringTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // If the key is not enter, return
            if(e.KeyCode != Keys.Enter)
            {
                return;
            }

            // Otherwise, split just like in the pattern text box
            splitText();
        }

        private void splitText()
        {
            // Get the pattern and string text from the different text boxes
            string pattern = patternTextBox.Text;
            string text = stringTextBox.Text;

            // Initialize the results text box to nothing
            resultsTextBox.Text = "";

            // If there is something wrong with the pattern the user typed in,
            // we output text to the results text box and return
            if(validPattern(pattern) == false)
            {
                resultsTextBox.Text = "Error: Invalid pattern.";
                return;
            }

            // Otherwise, we perform the split with whatever the user typed in
            Regex regexPattern = new Regex(pattern);

            string[] elts = regexPattern.Split(text);

            for (int i = 0; i < elts.Length; i++)
            {
                resultsTextBox.Text += elts[i];
                resultsTextBox.Text += "\r\n";
            }
        }

        private bool validPattern(string pattern)
        {
            bool isValid = true;
            Stack<char> parenthesis = new Stack<char>();

            // If the pattern contains a '*' and a '+' then we return false
            if(pattern.Contains('*') && pattern.Contains('+'))
            {
                return isValid = false;
            }

            // If the pattern only has a '+' or only has a '*' then we return false
            if(pattern == "+" || pattern == "*")
            {
                return isValid = false;
            }

            // If the pattern has a '?' and '+' then we return false
            if(pattern.Contains('?') && pattern.Contains('+'))
            {
                return isValid = false;
            }

            // If the pattern has a '?' and '*' then we return false
            if(pattern.Contains('?') && pattern.Contains('*'))
            {
                return isValid = false;
            }

            // Here we check to make sure if the user types in parenthesis, that they match
            for(int i = 0; i < pattern.Length; i++)
            {
                // If the user types in a closing brace first, we return false
                if (pattern[i] == ')' || pattern[i] == ']' || pattern[i] == '}')
                {
                    if (parenthesis.Count == 0)
                    {
                        return isValid = false;
                    }
                    parenthesis.Pop();
                }

                if (pattern[i] == '(' || pattern[i] == '[' || pattern[i] == '{')
                 {
                     parenthesis.Push(pattern[i]);
                 }

             }
            // If the stack is not empty by the end, there is an extra brace and we return false
             if(parenthesis.Count > 0)
             {
                 return isValid = false;
             }

            return isValid;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void resultsTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void stringTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
