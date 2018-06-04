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
        private Cards cards = new Cards();

        public Deck()
        {
            //cards = new Card [52];
            //int index = 0;

            for (int suitVal = 0; suitVal < 4; suitVal++)
            {
                for (int rankVal = 1; rankVal < 14; rankVal++)
                {
                    cards.Add(new Card((Suit)suitVal, (Rank)rankVal));
                    //cards[index] = new Card((Suit)suitVal, (Rank)rankVal);
                    //index++;
                }
                
            }
        }

        public Card GetCard(int cardNum)
        {
            if (cardNum>=0 && cardNum<=51)
            {
                return cards[cardNum];
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
            //bool[] assigned = new bool[52];
            Random sourceGen = new Random();

            //for (int i = 0; i < 52; i++)
            //{
            //    int destCard = 0;
            //    bool foundCard = false;

            //    while (foundCard == false)
            //    {
            //        destCard = sourceGen.Next(52);

            //        Debug.WriteLine(destCard);
            //        Debug.WriteLine(assigned[destCard]);

            //        if (assigned[destCard] == false)                   
            //        {
            //            foundCard = true;
            //        }
            //    }
            //    assigned[destCard] = true;
            //    newDeck[destCard] = cards[i];
            //}

            //for (int i = 0; i < 52; i++)
            //{
                int destCard = sourceGen.Next(52);
                int i = 0;

                //newDeck[destCard] = cards[i];
                //destCard = sourceGen.Next(52);


                while (i<52)
               
                {
                destCard = sourceGen.Next(52);

                
                if (!newDeck.Contains(GetCard(destCard)))
                {
                    newDeck.Add(cards[destCard]);
                    i++;
                }
                else
                {

                    destCard = sourceGen.Next(52);

                    if (!newDeck.Contains(GetCard(destCard)))
                    {
                        newDeck.Add(cards[destCard]);
                        i++;
                    }


                }

            }

            //}


            newDeck.CopyTo(cards);

        }






        //public void Shuffle2()
        //{
        //    Random rand = new Random();
        //    int removedCard;
        //    bool CardRemoved = false;
        //    List<Cards> newDeck = new List<Cards>();


        //    while (newDeck.Count < cards.Count)
        //    {
        //        CardRemoved = false;
        //        removedCard = rand.Next(1, 53);

        //        foreach (var item in newDeck)
        //        {
        //            if (item == cards[removedCard])
        //            {
        //                CardRemoved = true;
        //            }
        //        }

        //        if (CardRemoved == false)
        //        {
        //            newDeck.Add(cards[removedCard]);

        //        }
        //    }
        //}

    }
}