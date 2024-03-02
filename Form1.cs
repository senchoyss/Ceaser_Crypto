using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Caesar_CRYPTO
{
    public partial class Form1 : Form
    {
        private int usedKey; 
        public Form1()
        {
            InitializeComponent();
        }
        private string CaesarCipher(string text, int shift)
        {
            char[] resultChars = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                char currentChar = text[i];

                if (char.IsLetter(currentChar))
                {
                    char transformedChar = (char)(currentChar + shift);
                    if (currentChar >= 'А' && currentChar <= 'Я')
                    {
                        if (transformedChar > 'Я')
                            transformedChar = (char)(transformedChar - 32); 
                        else if (transformedChar < 'А')
                            transformedChar = (char)(transformedChar + 32); 
                    }
                    else if (currentChar >= 'а' && currentChar <= 'я')
                    {
                        if (transformedChar > 'я')
                            transformedChar = (char)(transformedChar - 32); 
                        else if (transformedChar < 'а')
                            transformedChar = (char)(transformedChar + 32); 
                    }

                    resultChars[i] = transformedChar;
                }
                else
                {
                    resultChars[i] = currentChar; 
                }
            }

            return new string(resultChars);
        }

        private int CalculateShift(char mostFrequentLetter)
        {
            return mostFrequentLetter - 'О'; 
        }

        private void DecryptAndShowResult()
        {
            string ciphertext = textBox2.Text.ToUpper();
            string decryptedText = CaesarCipher(ciphertext, -usedKey); 
            textBox4.Text = decryptedText; 
        }

        private void EncryptAndShowResult()
        {
            string plaintext = textBox1.Text.ToUpper();
            Dictionary<char, int> frequencyDictionary = CalculateFrequency(plaintext);
            char mostFrequentLetter = frequencyDictionary.OrderByDescending(pair => pair.Value).First().Key;
            usedKey = CalculateShift(mostFrequentLetter);
            string ciphertext = CaesarCipher(plaintext, usedKey);
            textBox2.Text = ciphertext;
            textBox3.Text = $"Used Key: {usedKey}";
        }

        private Dictionary<char, int> CalculateFrequency(string text)
        {
            Dictionary<char, int> frequencyDictionary = new Dictionary<char, int>();

            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {
                    if (frequencyDictionary.ContainsKey(c))
                        frequencyDictionary[c]++;
                    else
                        frequencyDictionary.Add(c, 1);
                }
            }

            return frequencyDictionary;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            EncryptAndShowResult();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DecryptAndShowResult();
        }
    }
}

