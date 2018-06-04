using Ch11CardLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UserFormPractice
{
    public class Hand
    {

        public delegate bool WinningHand(bool x);

        public Card currentHandCard1;
        public Card currentHandCard2;

        public Card highPairCard;
        public Card threeOfAKindCard;
        public Card fourOfAKindCard;

        public Card twoPairHighCard;
        public Card twoPairLowCard;

        public Card fullHouseThreeOfAKindCard;
        public Card fullHousePairCard;

        


        

        public Card straightHighCard;

        public Card straightFlushHighCard;

        public Card highFlushCard;

        #region HELPER METHODS VARIABLES

        public Card threeOfAKindCardInOpenFlop;

        public bool straightToAce = false;

        public Card twoPairInFlopHigherPairCard;

        public Card pairInHandCard;

        public Card pairInFlopCard;

        public Card currentHandHighCard;

        public Card smallerTwoPairCardInFlop;
        public Card higherTwoPairCardInFlop;

        public Card higherTwoPairCardInHand;
        public Card smallerTwoPairdCardInHand;

        
        #endregion



        public List<Card> OpenFlop = new List<Card>();



        //WinningCombination combination;



        public bool NoWinningHandFound()
        {
            if (!IsPair()&&!IsThreeOfAKind()&&!IsFourOfAKind()&&!IsFullHouse()
                &&!IsFlush()&&!IsStraightFlush()&&!IsRoyalFlush())
            {
                CurrentHandHighCard();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsPair()
        {
          

            int card1Count = 0;
            int card2Count = 0;

            var tempCardFlop = OpenFlop;
            tempCardFlop = tempCardFlop.OrderBy(c => c.rank).ToList();


            if (currentHandCard1.rank == currentHandCard2.rank)
            {
               
                highPairCard = currentHandCard1;
                
                foreach (var item in tempCardFlop)
                {
                    
                    if (item.rank == currentHandCard1.rank)
                    {
                        
                        highPairCard = null;
                        return false;
                       
                    }

                }

                return true;

                }

            else
            {
                foreach (var item in tempCardFlop)
                {
                    

                    if (item.rank == currentHandCard1.rank)
                    {
                        card1Count++;

                    }

                    if (item.rank == currentHandCard2.rank)
                    {
                        card2Count++;

                    }
                }

                
               if (card1Count == 1)
                {
                    highPairCard = currentHandCard1;
                    return true;
                }
                else if (card2Count == 1)
                {
                    highPairCard = currentHandCard2;
                    return true;
                }

                else
                {
                    return false;
                }

            }


        }

        public bool IsThreeOfAKind()
        {
            int card1Count = 1;
            int card2Count = 1;


            if (currentHandCard1.rank== currentHandCard2.rank)
            {
                //card1Count++;
                //card2Count++;

                foreach (var item in OpenFlop)
                {

                    if (item.rank == currentHandCard1.rank)
                    {
                        card1Count++;
                        
                    }

                   
                }

                if (card1Count == 2)
                {
                    
                    threeOfAKindCard = currentHandCard1;
                    return true;
                }
                
                else
                {
                    return false;
                }
            }



            else
            {
                foreach (var item in OpenFlop)
                {

                    if (item.rank == currentHandCard1.rank)
                    {
                        card1Count++;
                        //ThreeOfAKindCard = CurrentHandCard1;
                    }

                    if (item.rank == currentHandCard2.rank)
                    {
                        card2Count++;
                        //ThreeOfAKindCard = CurrentHandCard2;

                    }
                }

                if (card1Count == 3 && card2Count == 3)
                {
                    //bool findHighCard = CurrentHandCard1.rank > CurrentHandCard2.rank;

                    switch (currentHandCard1.rank > currentHandCard2.rank)
                    {
                        case true:
                            threeOfAKindCard = currentHandCard1;
                            break;
                        default:
                            threeOfAKindCard = currentHandCard2;
                            break;

                    }
                    return true;
                }
                else if (card1Count == 3)
                {
                    threeOfAKindCard = currentHandCard1;

                    return true;

                }
                else if (card2Count == 3)
                {
                    threeOfAKindCard = currentHandCard2;

                    return true;
                }
                else
                {
                    return false;
                }
                    
            }


        }

        public bool IsFourOfAKind()
        {
            int card1Count = 1;
            int card2Count = 1;

            if (currentHandCard1.rank == currentHandCard2.rank)
            {
                card1Count++;
                card2Count++;
                
            }

            foreach (var item in OpenFlop)
            {
                if (item.rank == currentHandCard1.rank)
                {
                    card1Count++;
                    fourOfAKindCard = currentHandCard1;
                }

                if (item.rank == currentHandCard2.rank)
                {
                    card2Count++;
                    
                    fourOfAKindCard = currentHandCard2;
                    
                }
            }

            if (card1Count == 4||card2Count==4)
            {
                return true;
            }
            else
            {
                return false;
            }



        }

        public bool IsTwoPairs()
        {
            Card pairInHandCard;
            Card largerTwoPairInFlop=null;
            Card smallerTwoPairInFlop=null;

            Card largerTwoPairInHand=null;
            Card smallerTwoPairInHand=null;

            IsPairinHand(out pairInHandCard);
            IsTwoPairInFlop(out largerTwoPairInFlop,out smallerTwoPairInFlop);
            IsTwoPairsInHand(out largerTwoPairInHand, out smallerTwoPairInHand);

            if (IsPairinHand() && IsTwoPairInFlop())
            {
                if (pairInHandCard.rank > smallerTwoPairInFlop.rank)
                {
                    //may need to improve here as pairInHandCard can be larger the the largerTwoPairInFlo
                    twoPairLowCard = pairInHandCard;
                    twoPairHighCard = largerTwoPairInFlop;
                }
                               

                return true;
            }

            else if (IsTwoPairInFlop() && IsPair())
            {
                if (highPairCard.rank > smallerTwoPairInFlop.rank)
                {
                    //may need to improve here as pairInHandCard can be larger the the largerTwoPairInFlo
                    twoPairLowCard = highPairCard;
                    twoPairHighCard = largerTwoPairInFlop;
                }
                return true;
            }

            //else if (largerTwoPairInFlop!=null&&largerTwoPairInHand!=null)
            //{
            //    if (largerTwoPairInFlop.rank>largerTwoPairInHand.rank)
            //    {
            //        twoPairHighCard = largerTwoPairInFlop;
            //        twoPairLowCard = largerTwoPairInHand;
            //    }
            //    else
            //    {
            //        twoPairHighCard = largerTwoPairInHand;
            //        twoPairLowCard = largerTwoPairInFlop;
            //    }
            //    return true;
            //}

           // else if (largerTwoPairInFlop != null && largerTwoPairInHand != null)
           else if(IsTwoPairsInHand())
            {
                //check if any pair in flop and check if its higher then the pairs in hand
                IsPairInFlop();

                if (pairInFlopCard != null && pairInFlopCard.rank ==Rank.Ace)
                {
                    twoPairHighCard = pairInFlopCard;
                    twoPairLowCard = largerTwoPairInHand;
                }

                else
                {
                    if (pairInFlopCard != null && pairInFlopCard.rank > largerTwoPairInHand.rank && largerTwoPairInHand.rank!=Rank.Ace)
                    {
                        twoPairHighCard = pairInFlopCard;
                        twoPairLowCard = largerTwoPairInHand;
                    }

                    else if (pairInFlopCard != null && pairInFlopCard.rank > smallerTwoPairInHand.rank)
                    {
                        twoPairHighCard = largerTwoPairInHand;
                        twoPairLowCard = pairInFlopCard;
                    }
                    else
                    {
                        twoPairHighCard = largerTwoPairInHand;
                        twoPairLowCard = smallerTwoPairInHand;
                    }   
                }

                return true;
            }


            else if (IsPairinHand() && IsPairInFlop())
            {
                if (pairInHandCard.rank > pairInFlopCard.rank)
                {
                    twoPairHighCard = pairInHandCard;
                    twoPairLowCard = pairInFlopCard;
                }
                else
                {
                    twoPairHighCard = pairInFlopCard;
                    twoPairLowCard = pairInHandCard;
                }


                return true;
            }
            else if (IsPair()&&IsPairInFlop())
            {
                if (highPairCard.rank>pairInFlopCard.rank)
                {
                    twoPairHighCard = highPairCard;
                    twoPairLowCard = pairInFlopCard;
                }
                else
                {
                    twoPairHighCard = pairInFlopCard;
                    twoPairLowCard = highPairCard;
                }

                return true;

            }

            

            else
            {
                return false;
            }

        }

        public bool IsFullHouse()
        {
            //check no three of kind in hand possible
            //check if three of kind in flop 
            //check if already a pair in hand and it is greater than a pair in flop
            //if (!IsThreeOfAKind() && IsThreeOfKindInFlop() && IsPairinHand() && pairInHandCard.rank>pairInFlopCard.rank)
            //{
            //    //if this is true then 3ofk card will be three 3ofk in open flop card & pair in hand rank
            //    fullHouseThreeOfAKindCard = threeOfAKindCardInOpenFlop;
            //    fullHousePairCard = pairInHandCard;
            //    return true;
            //}
            if (IsThreeOfKindInFlop()&&IsPairInFlop()&&IsPairinHand()&&
                pairInHandCard.rank > pairInFlopCard.rank)
            {
                fullHouseThreeOfAKindCard = threeOfAKindCardInOpenFlop;
                fullHousePairCard = pairInHandCard;

                //if (pairInHandCard.rank>pairInFlopCard.rank)
                //{
                //    fullHousePairCard = pairInHandCard;
                //}
                //else
                //{
                //    fullHousePairCard = pairInFlopCard;
                //}

                return true;

            }

            else if (IsThreeOfAKind()&&IsPair()&&IsTwoPairInFlop())
            {

                Card tempPairCard = null;

                fullHouseThreeOfAKindCard = threeOfAKindCard;

                if (higherTwoPairCardInFlop.rank==threeOfAKindCard.rank)
                {
                    tempPairCard = smallerTwoPairCardInFlop;
                }
                else
                {
                    tempPairCard = higherTwoPairCardInFlop;
                }

                if (highPairCard.rank>tempPairCard.rank)
                {
                    tempPairCard = highPairCard;
                }

                fullHousePairCard = tempPairCard;

                return true;


            }

            else if (IsThreeOfAKind() && IsPair())
            {
                fullHouseThreeOfAKindCard = threeOfAKindCard;
                fullHousePairCard = highPairCard;
                return true;
            }
            else if (IsThreeOfAKind()&&IsThreeOfKindInFlop()&&IsPairInFlop())
            {
                if (threeOfAKindCard.rank>threeOfAKindCardInOpenFlop.rank)
                {
                    fullHouseThreeOfAKindCard = threeOfAKindCard;
                    fullHousePairCard = threeOfAKindCardInOpenFlop;
                }
                else
                {
                    fullHouseThreeOfAKindCard = threeOfAKindCardInOpenFlop;
                    fullHousePairCard = threeOfAKindCard;
                }

                return true;
            }

            else if (IsThreeOfAKind() && IsTwoPairInFlop())
            {

                Card tempPairCard = null;

                fullHouseThreeOfAKindCard = threeOfAKindCard;

                if (higherTwoPairCardInFlop.rank == threeOfAKindCard.rank)
                {
                    tempPairCard = smallerTwoPairCardInFlop;
                }
                else
                {
                    tempPairCard = higherTwoPairCardInFlop;
                }

                fullHousePairCard = tempPairCard;

                return true;
            }

            else if (IsThreeOfKindInFlop()&&IsPair())
            {
                fullHouseThreeOfAKindCard = threeOfAKindCardInOpenFlop;
                fullHousePairCard = highPairCard;
                return true;

            }

            else
            {
                return false;
            }


            //}
            //catch (System.NullReferenceException)
            //{

            //    return false;
            //}

        }

        public bool IsStraight()
        {
           // List<Card> tempCardList = new List<Card>(OpenFlop);
           var tempCardList = new List<Card>(OpenFlop);


            tempCardList.Add(currentHandCard1);
            tempCardList.Add(currentHandCard2);

            //GroupsBy rank then filters unique ranks only using Select(x=>x.First())
            //and orders ascending by OrderBy
            tempCardList=tempCardList.GroupBy(x => x.rank)
                .Select(x=>x.First())
                .OrderBy(x=>x.rank).ToList();
            
            
            int consecutiveCardsCount = 0;

            if (IsStraightToAce())
            {
                straightToAce = true;
                return true;
            }
            else
            {


                for (int i = 0; i < tempCardList.Count - 1; i++)
                {
                    try
                    {
                        int nextCardDifference = tempCardList[i].rank - tempCardList[i + 1].rank;
                        int secondCardDifference = tempCardList[i].rank - tempCardList[i + 2].rank;
                        int thirdCardDifference = tempCardList[i].rank - tempCardList[i + 3].rank;

                        //if (nextCardDifference == -1 && secondCardDifference == -1)
                        //{
                        //    consecutiveCardsCount++;
                        //}

                        //if (nextCardDifference == -1 && secondCardDifference == -2)
                        //{
                        //    consecutiveCardsCount++;
                        //    straightHighCard = tempCardList[i + 2];
                        //}

                        if (nextCardDifference == -1 && secondCardDifference == -2&&thirdCardDifference==-3)
                        {
                            consecutiveCardsCount++;
                            straightHighCard = tempCardList[i + 3];
                        }


                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        continue;

                    }
                }

                if (consecutiveCardsCount >= 2)
                {
                    return true;
                }
                //else if (IsStraightToAce())
                //{
                //    straightToAce = true;
                //    return true;
                //}
                else
                {
                    return false;
                } 
            }


        }

        public bool IsFlush()
        {
            int suitMatchCountCard1 = 0;
            int suitMatchCountCard2 = 0;
           
            //possible flush is the suit that has chance of flush
            Suit possibleFlush;



            if (FlushPossible(OpenFlop,out possibleFlush))
            {
                if (currentHandCard1.suit==currentHandCard2.suit&&currentHandCard1.suit==possibleFlush)
                {
                    if (currentHandCard1.rank==Rank.Ace)
                    {
                        highFlushCard = currentHandCard1;
                    }

                   else  if (currentHandCard2.rank == Rank.Ace)
                    {
                        highFlushCard = currentHandCard2;
                    }

                    else if (currentHandCard1.rank>currentHandCard2.rank)
                    {
                        highFlushCard = currentHandCard1;
                    }
                    else
                    {
                        highFlushCard = currentHandCard2;
                    }

                    foreach (var item in OpenFlop)
                    {
                        if (item.rank==Rank.Ace&&item.suit==possibleFlush)
                        {
                            highFlushCard = item;
                        }
                        if (item.suit==possibleFlush && item.rank>highFlushCard.rank&&highFlushCard.rank!=Rank.Ace)
                        {
                            highFlushCard = item;
                        }

                    }

                    return true;
                }
                

               else if (currentHandCard1.suit==possibleFlush)
                {
                    highFlushCard = currentHandCard1;

                    foreach (var item in OpenFlop)
                    {
                        if (item.suit==possibleFlush)
                        {
                            suitMatchCountCard1++;

                            if (item.rank==Rank.Ace)
                            {
                                highFlushCard = item;
                            }
                            else if (item.rank > highFlushCard.rank && highFlushCard.rank!=Rank.Ace)
                            {
                                highFlushCard = item;
                            }
                            
                        }

                        
                    }
                }

                else if (currentHandCard2.suit == possibleFlush)
                {
                    highFlushCard = currentHandCard2;

                    foreach (var item in OpenFlop)
                    {
                        if (item.suit == possibleFlush)
                        {
                            suitMatchCountCard2++;

                            if (item.rank == Rank.Ace)
                            {
                                highFlushCard = item;
                            }
                            else if (item.rank > highFlushCard.rank && highFlushCard.rank != Rank.Ace)
                            {
                                highFlushCard = item;
                            }

                        }


                    }
                }

            }

            if (suitMatchCountCard1>=4||suitMatchCountCard2>=4)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool IsStraightFlush()
        {
            List<Card> tempCardList = new List<Card>(OpenFlop);


            // IOrderedEnumerable<Card> TempList;
            tempCardList.Add(currentHandCard1);
            tempCardList.Add(currentHandCard2);
            //TempList = OpenFlop.OrderBy(x => x.rank);
            tempCardList = tempCardList.OrderBy(x => x.rank).ToList();


            int consecutiveCardsCount = 0;
            int sameSuitCount = 0;

            //if (CurrentHandCard1.rank-CurrentHandCard2.rank==1|| CurrentHandCard1.rank - CurrentHandCard2.rank == -1)
            //{
            //    consecutiveCardsCount++;
            //}

            for (int i = 0; i < tempCardList.Count - 1; i++)
            {
                try
                {
                    int nextCardDifference = tempCardList[i].rank - tempCardList[i + 1].rank;
                    int secondCardDifference = tempCardList[i].rank - tempCardList[i + 2].rank;

                    if (nextCardDifference == -1 && secondCardDifference == -2)
                    {
                        consecutiveCardsCount++;
                        straightFlushHighCard= tempCardList[i + 2];

                        if (tempCardList[i].suit == tempCardList[i+1].suit && tempCardList[i].suit == tempCardList[i + 2].suit)
                        {
                            sameSuitCount++;
                        }
                    }


                }
                catch (System.ArgumentOutOfRangeException)
                {
                    continue;
                    // throw;
                }
            }

            if (consecutiveCardsCount > 2&&sameSuitCount>2)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public bool IsRoyalFlush()
        {
            List<Card> tempCardList = new List<Card>(OpenFlop);

            Suit royalFlushSuit;
            int remainingRoyalCardsCountFound = 0;

            
            //tempCardList.Add(CurrentHandCard1);
            //tempCardList.Add(CurrentHandCard2);
            
            //tempCardList = tempCardList.OrderBy(x => x.rank).ToList();

            if (currentHandCard1.rank==Rank.Ace&&currentHandCard2.rank==Rank.King
                &&currentHandCard2.suit==currentHandCard1.suit)
            {
                royalFlushSuit = currentHandCard1.suit;
                for (int i = 0; i < tempCardList.Count; i++)
                {
                    if (tempCardList[i].rank==Rank.Queen&&tempCardList[i].suit==royalFlushSuit)
                    {
                        remainingRoyalCardsCountFound++;
                    }
                    if (tempCardList[i].rank == Rank.Jack && tempCardList[i].suit == royalFlushSuit)
                    {
                        remainingRoyalCardsCountFound++;
                    }
                    if (tempCardList[i].rank == Rank.Ten && tempCardList[i].suit == royalFlushSuit)
                    {
                        remainingRoyalCardsCountFound++;
                    }

                    
                    
                }

                if (remainingRoyalCardsCountFound == 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            else if (currentHandCard1.rank==Rank.Ace||currentHandCard2.rank==Rank.Ace)
            {
                
                remainingRoyalCardsCountFound = 0;

                if (currentHandCard1.rank==Rank.Ace)
                {
                    royalFlushSuit = currentHandCard1.suit; 
                }
                else
                {
                    royalFlushSuit = currentHandCard2.suit;
                }

                for (int i = 0; i < tempCardList.Count; i++)
                {
                    if (tempCardList[i].rank == Rank.King && tempCardList[i].suit == royalFlushSuit)
                    {
                        remainingRoyalCardsCountFound++;
                    }
                    if (tempCardList[i].rank == Rank.Queen && tempCardList[i].suit == royalFlushSuit)
                    {
                        remainingRoyalCardsCountFound++;
                    }
                    if (tempCardList[i].rank == Rank.Jack && tempCardList[i].suit == royalFlushSuit)
                    {
                        remainingRoyalCardsCountFound++;
                    }
                    if (tempCardList[i].rank == Rank.Ten && tempCardList[i].suit == royalFlushSuit)
                    {
                        remainingRoyalCardsCountFound++;
                    }

                    
                }

                if (remainingRoyalCardsCountFound == 4)
                {
                    return true;
                }
                else
                {
                    return false;
                }
               

            }

            else if (currentHandCard1.rank==Rank.King||currentHandCard2.rank==Rank.King)
            {
                remainingRoyalCardsCountFound = 0;

                if (currentHandCard1.rank == Rank.King)
                {
                    royalFlushSuit = currentHandCard1.suit;
                }
                else
                {
                    royalFlushSuit = currentHandCard2.suit;
                }

                for (int i = 0; i < tempCardList.Count; i++)
                {
                    if (tempCardList[i].rank == Rank.Ace && tempCardList[i].suit == royalFlushSuit)
                    {
                        remainingRoyalCardsCountFound++;
                    }
                    if (tempCardList[i].rank == Rank.Queen && tempCardList[i].suit == royalFlushSuit)
                    {
                        remainingRoyalCardsCountFound++;
                    }
                    if (tempCardList[i].rank == Rank.Jack && tempCardList[i].suit == royalFlushSuit)
                    {
                        remainingRoyalCardsCountFound++;
                    }
                    if (tempCardList[i].rank == Rank.Ten && tempCardList[i].suit == royalFlushSuit)
                    {
                        remainingRoyalCardsCountFound++;
                    }


                }

                if (remainingRoyalCardsCountFound == 4)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }

            else
            {
                return false;
            }

            
        }

        #region HELPER METHODS
        public bool FlushPossible(List<Card> flopCards, out Suit possibleFlushSuite)
        {
            int club = 0;
            int diamond = 0;
            int heart = 0;
            int spade = 0;

            foreach (var item in flopCards)
            {

                switch (item.suit)
                {
                    case Suit.Club:
                        club++;
                        break;
                    case Suit.Diamond:
                        diamond++;
                        break;
                    case Suit.Heart:
                        heart++;
                        break;
                    case Suit.Spade:
                        spade++;
                        break;

                        //default:
                        //    break;
                }


            }

            if (club >= 3)
            {
                possibleFlushSuite = Suit.Club;
                return true;
            }
            else if (diamond >= 3)
            {
                possibleFlushSuite = Suit.Diamond;
                return true;
            }
            else if (heart >= 3)
            {
                possibleFlushSuite = Suit.Heart;
                return true;
            }
            else if (spade >= 3)
            {
                possibleFlushSuite = Suit.Spade;
                return true;
            }
            else
            {
                possibleFlushSuite = Suit.None;
                return false;
            }
        }

        public void CurrentHandHighCard()
        {
            if (currentHandCard1.rank==Rank.Ace)
            {
                currentHandHighCard = currentHandCard1;
            }
            else if (currentHandCard2.rank == Rank.Ace)
            {
                currentHandHighCard = currentHandCard2;
            }
            else if (currentHandCard1.rank>currentHandCard2.rank)
            {
                currentHandHighCard = currentHandCard1;
            }
            else if (currentHandCard1.rank == currentHandCard2.rank)
            {
                currentHandHighCard = currentHandCard1;
            }
            else
            {
                currentHandHighCard = currentHandCard2;

            }
        }
        public bool IsPairinHand()
        {
            if (currentHandCard1.rank==currentHandCard2.rank)
            {
                pairInHandCard =currentHandCard1;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void IsPairinHand(out Card pairCard)
        {
            if (currentHandCard1.rank == currentHandCard2.rank)
            {
                pairCard = currentHandCard1;
                                
            }
            else
            {
                pairCard = null;
            }
            
        }
        public bool IsPairInFlop()
        {

            //calling this to check if there is a 3ofK card in flop so as to ignore it in the count
            IsThreeOfKindInFlop();
               
            int pairFoundCount = 0;
            //track if more than 2 pairs found which will invalidate the pair find
            int totalPairsFoundCount = 0;
            Card tempHighPairCard=null;
            

            for (int j = 0; j < OpenFlop.Count; j++)
            {
                for (int i = j+1; i < OpenFlop.Count; i++)
                {
                    if (OpenFlop[i].rank==OpenFlop[j].rank)
                    {
                        pairFoundCount++;
                        
                    }
                }
                //checks if 3ofk in flop is nt null and if pair card found is 
                //part of a three of kind and skips if it is
                if (threeOfAKindCardInOpenFlop!=null && pairFoundCount==1 && 
                    OpenFlop[j].rank!=threeOfAKindCardInOpenFlop.rank)
                {
                    tempHighPairCard = OpenFlop[j];
                    totalPairsFoundCount++;
                    
                }
               else if (pairFoundCount == 1)
                {
                    tempHighPairCard = OpenFlop[j];
                    totalPairsFoundCount++;

                }


                //clear the pair found count to start searching for the next one
                pairFoundCount = 0;
            }

            
            if (totalPairsFoundCount == 1)
            {
                pairInFlopCard = tempHighPairCard;
                return true;
            }

            else
            {
                return false;
            }
        }

        public bool IsTwoPairsInHand()
        {
            int smallerTwoPairCardCount = 1;
            int higherTwoPairCardCount = 1;


            var tempCardFlop = OpenFlop;
            tempCardFlop.OrderBy(c => c.rank);

            var cardsInHand = new List<Card> { currentHandCard1, currentHandCard2 };

            cardsInHand = cardsInHand.OrderBy(c => c.rank).ToList();

            foreach (var item in tempCardFlop)
            {
                if (item.rank == cardsInHand[0].rank)
                {
                    smallerTwoPairCardCount++;
                    smallerTwoPairdCardInHand = cardsInHand[0];

                }

                if (item.rank == cardsInHand[1].rank)
                {
                    higherTwoPairCardCount++;
                    higherTwoPairCardInHand = cardsInHand[1];

                }


            }

            if (smallerTwoPairCardCount == 2 && higherTwoPairCardCount == 2)
            {
                //smallerTwoPairdCardInHand = cardsInHand[0];
                //higherTwoPairCardInHand = cardsInHand[1];
                //return true;

                if (cardsInHand[0].rank==Rank.Ace)
                {
                    higherTwoPairCardInHand = cardsInHand[0];
                    smallerTwoPairdCardInHand= cardsInHand[1];
                    return true;
                }
                else if (cardsInHand[1].rank == Rank.Ace)
                {
                    higherTwoPairCardInHand = cardsInHand[1];
                    smallerTwoPairdCardInHand = cardsInHand[0];
                    return true;
                }
                else
                {
                    smallerTwoPairdCardInHand = cardsInHand[0];
                    higherTwoPairCardInHand = cardsInHand[1];
                    return true;
                }
            }


            else
            {
                return false;
            }
        }
        public void IsTwoPairsInHand(out Card higherTwoPairCardInHand, out Card smallerTwoPairdCardInHand)
        {
            int smallerTwoPairCardCount = 1;
            int higherTwoPairCardCount = 1;


            var tempCardFlop = OpenFlop;
            tempCardFlop.OrderBy(c => c.rank);

            var cardsInHand = new List<Card> { currentHandCard1, currentHandCard2 };

            cardsInHand = cardsInHand.OrderBy(c => c.rank).ToList();

            foreach (var item in tempCardFlop)
            {
                if (item.rank == cardsInHand[0].rank)
                {
                    smallerTwoPairCardCount++;

                }

                if (item.rank == cardsInHand[1].rank)
                {
                    higherTwoPairCardCount++;
                    //higherTwoPairCardInHand = cardsInHand[1];

                }


            }

            if (smallerTwoPairCardCount == 2 && higherTwoPairCardCount == 2)
            {
                //smallerTwoPairdCardInHand = cardsInHand[0];
                //higherTwoPairCardInHand = cardsInHand[1];

                if (cardsInHand[0].rank==Rank.Ace)
                {
                    smallerTwoPairdCardInHand = cardsInHand[1];
                    higherTwoPairCardInHand = cardsInHand[0];
                }
                else
                {
                    smallerTwoPairdCardInHand = cardsInHand[0];
                    higherTwoPairCardInHand = cardsInHand[1];
                }


            }
            else
            {
                smallerTwoPairdCardInHand = null;
                higherTwoPairCardInHand = null;
            }

            
        }



        public bool IsTwoPairInFlop()
        {
            var tempCardFlop = OpenFlop;
            tempCardFlop=tempCardFlop.OrderBy(c => c.rank).ToList();
            int twoPairFoundCount = 0;
           // int pairFoundCount = 0;


            Card tempSmallerTwoPairCard = null;
            Card tempHigerTwoPairCard = null;

            for (int j = 0; j < tempCardFlop.Count; j++)
            {
                for (int i = j + 1; i < tempCardFlop.Count; i++)
                {
                    if (tempCardFlop[i].rank == tempCardFlop[j].rank)
                    {
                        //pairFoundCount++;
                        
                        if (twoPairFoundCount == 0)
                        {
                            twoPairFoundCount++;
                            tempSmallerTwoPairCard = tempCardFlop[j];
                        }
                        else
                        {
                            twoPairFoundCount++;
                            tempHigerTwoPairCard = tempCardFlop[j];
                        }

                    }
                }

                //if (pairFoundCount == 1)
                //{
                //    twoPairFoundCount++;
                //}
            }

            if (twoPairFoundCount == 2)
            {
                //smallerTwoPairCardInFlop=tempSmallerTwoPairCard;
                //higherTwoPairCardInFlop=tempHigerTwoPairCard;
                //return true;
                if (tempSmallerTwoPairCard.rank==Rank.Ace)
                {
                    higherTwoPairCardInFlop = tempSmallerTwoPairCard;
                    smallerTwoPairCardInFlop = tempHigerTwoPairCard;
                    return true;
                }
                else
                {
                    smallerTwoPairCardInFlop = tempSmallerTwoPairCard;
                    higherTwoPairCardInFlop = tempHigerTwoPairCard;
                    return true;
                }
            }
            else
            {
                return false;
            }

        }

        public void IsTwoPairInFlop(out Card higherTwoPairCardInFlop,out Card smallerTwoPairCardInFlop)
        {
            var tempCardFlop = OpenFlop;
            tempCardFlop=tempCardFlop.OrderBy(c => c.rank).ToList();
            int twoPairFoundCount = 0;
            int pairFoundCount = 0;


            Card tempSmallerTwoPairCard = null;
            Card tempHigerTwoPairCard = null;

            for (int j = 0; j < tempCardFlop.Count; j++)
            {
                for (int i = j + 1; i < tempCardFlop.Count; i++)
                {
                    if (tempCardFlop[i].rank == tempCardFlop[j].rank)
                    {
                       // pairFoundCount++;
                        if (twoPairFoundCount == 0)
                        {
                            twoPairFoundCount++;
                            tempSmallerTwoPairCard = tempCardFlop[j];
                        }
                        else
                        {
                            twoPairFoundCount++;
                            tempHigerTwoPairCard = tempCardFlop[j];
                        }

                    }
                }

                //if (pairFoundCount == 1)
                //{
                //    twoPairFoundCount++;
                //}
            }

            if (twoPairFoundCount == 2)
            {
                //smallerTwoPairCardInFlop=tempSmallerTwoPairCard;
                //higherTwoPairCardInFlop=tempHigerTwoPairCard;
                if (tempSmallerTwoPairCard.rank==Rank.Ace)
                {
                    higherTwoPairCardInFlop = tempSmallerTwoPairCard;
                    smallerTwoPairCardInFlop = tempHigerTwoPairCard;
                }
                else
                {
                    smallerTwoPairCardInFlop = tempSmallerTwoPairCard;
                    higherTwoPairCardInFlop = tempHigerTwoPairCard;
                }

            }
            else
            {
                smallerTwoPairCardInFlop = null;
                higherTwoPairCardInFlop = null;
                
            }

        }
        public bool IsThreeOfKindInFlop()
        {
            int threeCardsFoundCount = 0;

            for (int i = 0; i < OpenFlop.Count; i++)
            {


                if (threeCardsFoundCount == 3)
                {
                    break;
                }

                if (threeCardsFoundCount != 3)
                {
                    threeCardsFoundCount = 0;
                }

                try
                {
                    foreach (var item in OpenFlop)
                    {
                        if (OpenFlop[i].rank == item.rank)
                        {
                            threeCardsFoundCount++;
                        }

                        if (threeCardsFoundCount == 3)
                        {
                            threeOfAKindCardInOpenFlop = item;
                            break;
                        }
                    }



                }
                catch (System.IndexOutOfRangeException)
                {

                    continue;
                }

            }

            if (threeCardsFoundCount == 3)
            {
                return true;
            }

            else
            {
                return false;
            }

            
           

        }

        
        public bool IsFullHouseInFlop()
        {
            if (IsThreeOfKindInFlop()&&IsPairInFlop())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsStraightToAce()
        {
            List<Card> tempCardList = new List<Card>(OpenFlop);

            int remainingStraightCardsFoundCount = 0;


            //tempCardList.Add(CurrentHandCard1);
            //tempCardList.Add(CurrentHandCard2);

            //tempCardList = tempCardList.OrderBy(x => x.rank).ToList();

            if ((currentHandCard1.rank == Rank.Ace && 
                currentHandCard2.rank == Rank.King)||
                (currentHandCard1.rank==Rank.King&&
                currentHandCard2.rank==Rank.Ace))
            {

                for (int i = 0; i < tempCardList.Count; i++)
                {
                    if (tempCardList[i].rank == Rank.Queen)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.Jack)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.Ten)
                    {
                        remainingStraightCardsFoundCount++;
                    }



                }

                if (remainingStraightCardsFoundCount == 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else if (currentHandCard1.rank == Rank.Ace || currentHandCard2.rank == Rank.Ace)
            {

                remainingStraightCardsFoundCount = 0;

                for (int i = 0; i < tempCardList.Count; i++)
                {
                    if (tempCardList[i].rank == Rank.King)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.Queen)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.Jack)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.Ten)
                    {
                        remainingStraightCardsFoundCount++;
                    }


                }

                if (remainingStraightCardsFoundCount == 4)
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }

            else if (currentHandCard1.rank == Rank.King || currentHandCard2.rank == Rank.King)
            {
                remainingStraightCardsFoundCount = 0;


                for (int i = 0; i < tempCardList.Count; i++)
                {
                    if (tempCardList[i].rank == Rank.Ace)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.Queen)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.Jack)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.Ten)
                    {
                        remainingStraightCardsFoundCount++;
                    }


                }

                
                if (remainingStraightCardsFoundCount == 4)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            else if (currentHandCard1.rank == Rank.Queen || currentHandCard2.rank == Rank.Queen)
            {
                remainingStraightCardsFoundCount = 0;


                for (int i = 0; i < tempCardList.Count; i++)
                {
                    if (tempCardList[i].rank == Rank.Ace)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.King)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.Jack)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.Ten)
                    {
                        remainingStraightCardsFoundCount++;
                    }


                }


                if (remainingStraightCardsFoundCount == 4)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            else if (currentHandCard1.rank == Rank.Jack || currentHandCard2.rank == Rank.Jack)
            {
                remainingStraightCardsFoundCount = 0;


                for (int i = 0; i < tempCardList.Count; i++)
                {
                    if (tempCardList[i].rank == Rank.Ace)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.King)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.Queen)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.Ten)
                    {
                        remainingStraightCardsFoundCount++;
                    }


                }


                if (remainingStraightCardsFoundCount == 4)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            else if (currentHandCard1.rank == Rank.Ten || currentHandCard2.rank == Rank.Ten)
            {
                remainingStraightCardsFoundCount = 0;


                for (int i = 0; i < tempCardList.Count; i++)
                {
                    if (tempCardList[i].rank == Rank.Ace)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.King)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.Queen)
                    {
                        remainingStraightCardsFoundCount++;
                    }
                    if (tempCardList[i].rank == Rank.Jack)
                    {
                        remainingStraightCardsFoundCount++;
                    }


                }


                if (remainingStraightCardsFoundCount == 4)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            else
            {
                return false;
            }


        }


        #endregion
        public void FindWinner()//(List<Card> OpenFlop, WinningHand hand, string msg1="")
        {
            if (IsRoyalFlush())
            {
                MessageBox.Show("Won by a Royal Flush");

            }

            else if (IsStraightFlush())
            {
                MessageBox.Show("Won by straight Flush to " + straightHighCard.rank.ToString());

            }

            else if (IsFourOfAKind())
            {
                MessageBox.Show("Won by Four of Kind of " + fourOfAKindCard.rank.ToString());
            }

            
            else if (IsFullHouse())
            {
                
                    MessageBox.Show("Won by a Full House "+fullHouseThreeOfAKindCard.rank+" " +"full of "+ fullHousePairCard.rank.ToString());
                              
            }

            
            
            else if (IsFlush())
            {
                MessageBox.Show("Won by a Flush " + highFlushCard.rank.ToString() + " High");
            }


            else if (IsStraight())
            {
                if (straightToAce==true)
                {
                    MessageBox.Show("Won by straight to Ace");

                }
                else
                {
                    MessageBox.Show("Won by straight to " + straightHighCard.rank.ToString());

                }
            }

            else if (IsThreeOfAKind())
            {
                MessageBox.Show("Won by Three of Kind of " + threeOfAKindCard.rank.ToString());
            }

            else if (IsTwoPairs())
            {
                //if (twoPairLowCard!=null&&twoPairHighCard!=null)
                //{
                //    MessageBox.Show("Won by Two Pairs of " +twoPairLowCard.rank + " "+twoPairHighCard.rank);
                //}
                //else
                //{
                //    MessageBox.Show("Won by Two Pairs of " + smallerTwoPairCardInFlop.rank + " " + higherTwoPairCardInFlop.rank);

                //}

                MessageBox.Show("Won by Two Pairs of " + twoPairLowCard.rank + " " + twoPairHighCard.rank);


            }

            else if (IsPair())
            {
                MessageBox.Show("Won by a pair of " + highPairCard.rank.ToString());
            }

            else if(NoWinningHandFound())
            {
                MessageBox.Show("Won by High Card " + currentHandHighCard.rank);

            }


        }

        

    }
}
