using Ch11ClassLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Ch11ClassLib;

namespace Ch11CardLib
{
    public class Deck
    {
        public readonly Cards CurrentDeck = new Cards();

        public Deck()
        {
            //cards = new Card [52];
            //int index = 0;

            for (int suitVal = 0; suitVal < 4; suitVal++)
            {
                for (int rankVal = 1; rankVal < 14; rankVal++)
                {
                    CurrentDeck.Add(new Card((Suit)suitVal, (Rank)rankVal));
                    //cards[index] = new Card((Suit)suitVal, (Rank)rankVal);
                    //index++;
                }
                
            }
        }

        public Card GetCard(int cardNum)
        {
            if (cardNum>=0 && cardNum<=51)
            {
                return CurrentDeck[cardNum];
            }
            else
            {
                throw (new System.ArgumentOutOfRangeException("cardNum", cardNum, 
                    "value must be between 0 and 51"));
            }
        }


        public void Shuffle()
        {
            Cards newDeck = new Cards();
          
            Random sourceGen = new Random();

           
                int destCard = sourceGen.Next(52);
                int i = 0;

               

                while (i<52)
               
                {
                destCard = sourceGen.Next(52);

                
                if (!newDeck.Contains(GetCard(destCard)))
                {
                    newDeck.Add(CurrentDeck[destCard]);
                    i++;
                }
                else
                {

                    destCard = sourceGen.Next(52);

                    if (!newDeck.Contains(GetCard(destCard)))
                    {
                        newDeck.Add(CurrentDeck[destCard]);
                        i++;
                    }


                }

            }
                    


            newDeck.CopyTo(CurrentDeck);

        }

      
    }
}