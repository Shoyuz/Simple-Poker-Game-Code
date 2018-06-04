using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ch11CardLib;

namespace UserFormPractice
{
    public partial class Form1 : Form
    {

        public void CompareCard(Card card1,Card card2)
        {
                        
            if (card1.rank == card2.rank)
            {
                MessageBox.Show("Match");
            }
            else
            {
                MessageBox.Show("No Match");
            }

        }


        public Form1()
        {
            InitializeComponent();
        }

        #region Public Variables
        Deck d1;//= new Ch11CardLib.Deck();
        Player p1;
        Hand currentHand = new Hand();

        int i = 0;
        int player1Card1index;
        int player1Card2index;
        int dealCard1index;
        int dealCard2Index;
        int dealCard3Index;
        int dealCard4Index;
        int dealCard5Index;

        string cardImageFolderPath = @"C:\Users\zap\Desktop\cards\Cards\";
        #endregion
        


        private void dealButton_Click(object sender, EventArgs e)
        {

          d1 = new Deck();

            //  i = 0;

            dealCard1index = 23;
            dealCard2Index = 38;
            dealCard3Index = 46;
            dealCard4Index = 0;
            dealCard5Index = 24;
            player1Card1index = 35;
            player1Card2index = 29;

            //dealCard1index = 0;
            //dealCard2Index = 1;
            //dealCard3Index = 2;
            //dealCard4Index = 3;
            //dealCard5Index = 4;
            //player1Card1index = 5;
            //player1Card2index = 6;

            currentHand.OpenFlop.Clear();

            currentHand.highPairCard = null;
            currentHand.threeOfAKindCard = null;
            currentHand.fourOfAKindCard = null;
            currentHand.straightHighCard = null;
            currentHand.straightFlushHighCard = null;
            currentHand.highFlushCard = null;
            currentHand.higherTwoPairCardInHand = null;
            currentHand.smallerTwoPairdCardInHand = null;
            currentHand.straightToAce = false;
            currentHand.threeOfAKindCardInOpenFlop = null;
            currentHand.twoPairInFlopHigherPairCard = null;
            currentHand.pairInHandCard = null;
            currentHand.currentHandHighCard = null;
            currentHand.twoPairLowCard = null;
            currentHand.twoPairHighCard = null;
            currentHand.smallerTwoPairCardInFlop = null;
            currentHand.higherTwoPairCardInFlop = null;
            currentHand.fullHouseThreeOfAKindCard = null;
            currentHand.fullHousePairCard = null;
            currentHand.pairInFlopCard = null;
            

            // Random cardsOnTable = new Random();

           //d1.Shuffle();




            dealCard1Label.Text = d1.GetCard(dealCard1index).ToString();
            card1PictureBox.Load(cardImageFolderPath + d1.GetCard(dealCard1index).ToString() + ".png");
            currentHand.OpenFlop.Add(d1.GetCard(dealCard1index));
            //i++;

            dealCard2Label.Text = d1.GetCard(dealCard2Index).ToString();
            card2PictureBox.Load(cardImageFolderPath + d1.GetCard(dealCard2Index).ToString() + ".png");
            currentHand.OpenFlop.Add(d1.GetCard(dealCard2Index));
            //i++;

            dealCard3Label.Text = d1.GetCard(dealCard3Index).ToString();
            card3PictureBox.Load(cardImageFolderPath + d1.GetCard(dealCard3Index).ToString() + ".png");
            currentHand.OpenFlop.Add(d1.GetCard(dealCard3Index));
            //i++;

            //testcoderemovelater
            dealCard4Label.Text = d1.GetCard(dealCard4Index).ToString();
            card4PictureBox.Load(cardImageFolderPath + d1.GetCard(dealCard4Index).ToString() + ".png");
            currentHand.OpenFlop.Add(d1.GetCard(dealCard4Index));
           // i++;

            dealCard5Label.Text = d1.GetCard(dealCard5Index).ToString();
            card5PictureBox.Load(cardImageFolderPath + d1.GetCard(dealCard5Index).ToString() + ".png");
            currentHand.OpenFlop.Add(d1.GetCard(dealCard5Index));
            //i++;
            //testcode

            listBox1.Items.Clear();

            //foreach (var item in currentHand.OpenFlop)
            //{
            //    listBox1.Items.Add(item);
            //}

            for (int i = 0; i < d1.CurrentDeck.Count; i++)
            {
                listBox1.Items.Add(i.ToString()+" "+d1.CurrentDeck[i]);
            }

            
            player1Card1.Text = d1.GetCard(player1Card1index).ToString();
            player1Card1PictureBox.Load(cardImageFolderPath + d1.GetCard(player1Card1index).ToString() + ".png");
            currentHand.currentHandCard1 = d1.GetCard(player1Card1index);//new Card(d1.GetCard(i).suit, d1.GetCard(i).rank);
            //i++;

            player1Card2.Text = d1.GetCard(player1Card2index).ToString();
            player1Card2PictureBox.Load(cardImageFolderPath + d1.GetCard(player1Card2index).ToString() + ".png");
            currentHand.currentHandCard2 = d1.GetCard(player1Card2index);
           // i++;





            // MessageBox.Show(currentHand.CurrentHand().ToString());
            currentHand.FindWinner();



        }

        private void betButton_Click(object sender, EventArgs e)
        {

            if (dealCard4Label.Text == "Card4")
            {
                
                dealCard4Label.Text = d1.GetCard(dealCard4Index).ToString();
                card4PictureBox.Load(cardImageFolderPath + d1.GetCard(dealCard4Index).ToString() + ".png");
                currentHand.OpenFlop.Add(d1.GetCard(dealCard4Index));
                //i++;
               
            }

            else 

            {
                
                dealCard5Label.Text = d1.GetCard(dealCard5Index).ToString();
                card5PictureBox.Load(cardImageFolderPath + d1.GetCard(dealCard5Index).ToString() + ".png");
                currentHand.OpenFlop.Add(d1.GetCard(dealCard5Index));
                //i++;

            }

        }

        private void checkButton_Click(object sender, EventArgs e)
        {
           
            betButton_Click(sender, new System.EventArgs());
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            p1 = new Player(player1NameTextBox1.Text);
        }

        
    }
}
