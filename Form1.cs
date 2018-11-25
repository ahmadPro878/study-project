using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       // bool[] visted;
     //   Stack<int> stack1; 
        string[] EndVertex1, EndVertex2, AllVertex;
        int NumOfEdges, NumofVertex;
        List<string> linkedList = new List<string>();
        int[,] IncedenceMatrix;

        List<string> queueString=new List<string>();
        List<int> queueInt = new List<int>();

        List<string> final = new List<string>();

        bool remove;

        //capture child while serching BFS
        List<string> process = new List<string>();
        public void Read_Form_text()
        {
            string line;
            string[] lineArray;
            int counter = 0;
            try
            {
                StreamReader sr = new StreamReader("Graph.txt") ; //don't forget "using System.IO;"
                
                
                //Read all line to get the number of edge where eash line represent an edge
                NumOfEdges = File.ReadAllLines("Graph.txt").Length - 1; //lenght-1 because the vertix line
                //read the first line of the text
                line = sr.ReadLine();   //here read the first line from the text
                AllVertex = line.Split(':');
                NumofVertex = AllVertex.Length;

                EndVertex1 = new string[NumOfEdges];
                EndVertex2 = new string[NumOfEdges];

                line = sr.ReadLine(); //here he read the scound one,someway he incresed the read to the next line

                while (line != null)
                {
                    lineArray = line.Split(',');
                                          /* Trim() will removes all whitespace characters
                                              * from the beginning and end of the string*/
                    EndVertex1[counter] = lineArray[0].Trim(); 
                    EndVertex2[counter] = lineArray[1].Trim();

                    line = sr.ReadLine();
                    counter++;
                }
                sr.Close();
               
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message);
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            Read_Form_text();
            for (int i = 0; i < AllVertex.Length; i++)
            {
                richTextBox1.Text += AllVertex[i] + "\t";
                
            }
            richTextBox1.Text += "\n";
            for (int i = 0; i < EndVertex1.Length; i++)
            {
                richTextBox1.Text += EndVertex1[i] + "\t" + EndVertex2[i] + "\n";
            }
            

        }
        /*** GET INCEDENCE MATRIX ***/
        void getInedence()
        {
            IncedenceMatrix = new int[NumofVertex, NumOfEdges];
            for (int i = 0; i < EndVertex1.Length; i++)
            {
                /*because the matrix index start on "0" 
              we decreases 1 from the given number of the text file*/
                int end1 = Convert.ToInt32(EndVertex1[i]) - 1;
                int end2 = Convert.ToInt32(EndVertex2[i]) - 1;
                IncedenceMatrix[end1, i] = 1;
                IncedenceMatrix[end2, i] = 1;
            }
        }

        /*** GET ADJACENCY LINKED LIST ***/

        public List<string> getAdjList(string node)
        {
            List<string> myList = new List<string>();
            for (int i = 0; i < EndVertex1.Length; i++)
            {
                if (node == EndVertex1[i])
                    if (!myList.Contains(EndVertex2[i]))//already added??!
                    {
                        myList.Add(EndVertex2[i]);
                    }


            }
            for (int i = 0; i < EndVertex2.Length; i++)
            {
                if (node == EndVertex2[i])
                    if (!myList.Contains(EndVertex1[i]))//already added??!
                    {
                        myList.Add(EndVertex1[i]);
                    }
            }
            /*
            for (int i = 0; i < EndVertex1.Length; i++)
            {
                if ((!(node == EndVertex1[i])) && (!(node == EndVertex2[i])))
                    myList.Add("null");

            }
           */
            return myList;

        }
        /*-------- GET INDEX------*/
     //  int getIndex()
     //   {


      //  }

        /*****BFS*****/
        public void BFS(string s)
        {
            string temp;
            // Mark all the vertices as not visited
            bool[] visited = new bool[NumofVertex];
            for (int i = 0; i < NumofVertex; i++)
            {
                visited[i] = false;
            }

            //Create a queue for BFS
            
            visited[0] = true;
            queueString.Add(s);
           

            process = getAdjList(s); // we have the sun list,like 1-->5 2
            for (int i = 0; i < process.Count; i++)
            {
                 queueString.Add(process.ElementAt(i)); 
            }

            /*-------- for tracker and display---------*/
            richTextBox2.Text += ("process={");
            for (int i = 0; i < process.Count; i++)
            {
                richTextBox2.Text += (process.ElementAt(i) + "\t");
            }
            richTextBox2.Text += ("}\n");

            /*----------------------------------------*/
            final.Add(queueString.ElementAt(0));
            queueString.Remove(queueString.ElementAt(0));
         
            temp = queueString.ElementAt(0);
            final.Add(queueString.ElementAt(0));
            queueString.Remove(queueString.ElementAt(0));
            
            process = getAdjList(temp);
            for (int i = 0; i < process.Count; i++)
            {
                queueString.Add(process.ElementAt(i));
            }

           
            temp = queueString.ElementAt(0);
            final.Add(queueString.ElementAt(0));
            queueString.Remove(queueString.ElementAt(0));

            process = getAdjList(temp);
            for (int i = 0; i < process.Count; i++)
            {
                queueString.Add(process.ElementAt(i));
            }

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            button6.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "Incedence Matrix is : \n";
            getInedence();
            for (int i = 0; i < NumofVertex; i++)
            {
                for (int j = 0; j < NumOfEdges; j++)
                {
                    richTextBox1.Text += IncedenceMatrix[i, j] + "\t";
                }
                richTextBox1.Text += "\n";
                
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {

            List<string> list = new List<string>();
            list = getAdjList(textBox1.Text);/* this list is the adjacency list*/

           
            richTextBox1.Text += "\n"+textBox1.Text+"\t";
            for (int i = 0; i < list.Count; i++)
            {
                richTextBox1.Text += list.ElementAt(i) + " ";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();


            for (int i = 0; i < NumofVertex; i++)
            {
                list = getAdjList(AllVertex.ElementAt(i));
                
                
                richTextBox1.Text += "\n" + AllVertex.ElementAt(i) + "\t";
                for (int j = 0; j < list.Count; j++)
                {

                    richTextBox1.Text += list.ElementAt(j) + " ";

               //     linkedList.Add(list.ElementAt(j));
                } 
           //     linkedList.Add(" ");
                //    list.Clear();
            
            }
           /* tring to get all list element but there weap
            foreach (string item in list)
            {
                richTextBox2.Text += item ;
            }*/
        }

        //get the adjacncy vertix to the given one in textBox,using a list this time
        private void button5_Click(object sender, EventArgs e)
        {
       //     richTextBox2.Text+=linkedList.ElementAt(Convert.ToInt32(textBox2.Text)+1);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string temp;
            temp = textBox3.Text;
            BFS(temp);
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            button6.Enabled = true;
            
        }
    }
}
