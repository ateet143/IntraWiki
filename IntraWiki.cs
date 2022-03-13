using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Runtime.Serialization;

/// Created by Atit
/// Version 3
/// This application is a prototype of the wikipedia, application has used 2 dimension array to store the 12 Data Structure information,However user can able to ADD, EDIT and DELETE the information in the application.
/// Similarly, application open and saves file in the home folder, additionally user can able to sort and search the data by entering data in the textbox.

namespace IntraWiki
{
    public partial class IntraWiki : Form
    {
        #region CONSTRUCT AND GLOBAL VARIABLE
        public IntraWiki()
        {
            InitializeComponent();
        }

        // 8.1	Create a global 2D string array, use static variables for the dimensions (row, column),
        static int rowSize = 12;
        static int colSize = 4;
        static string[,] myArray = new string[rowSize, colSize];

        string defaultFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Definitions.bin");
        string currentFile;                                     //Setting global variable as filename, can be used for saving or renaming file while saving
     
        #endregion

        #region DISPLAY,APPLICATION AUTO LOAD, DATA AUTO LOAD

      //  8.6	Create a display method that will show the following information in a List box: Name and Category,
        private void DisplayArray()
        {
            listBoxWiki.Items.Clear();
            listBoxCategory.Items.Clear();

            for (int x = 0; x < rowSize; x++)
            {
                //if the array has null or empty then it will not further process and list display. Helpful to optimise and prevent empty string in listbox
                if (string.IsNullOrEmpty(myArray[x, 0]))
                {
                    return;
                }

                listBoxWiki.Items.Add(myArray[x, 0]);
                listBoxCategory.Items.Add(myArray[x, 1]);
            }
        }

        //8.9	Create a LOAD button that will read the information from a binary file called definitions.dat into the 2D array,
        private void IntraWiki_Load(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogVG = new OpenFileDialog();
            openFileDialogVG.InitialDirectory = defaultFileName;
            openFileDialogVG.Filter = "BIN Files|*.bin";
            openFileDialogVG.Title = "Select a BIN File";
            if (openFileDialogVG.ShowDialog() == DialogResult.OK)
            {
                currentFile = openFileDialogVG.FileName;
                openRecord(currentFile);
                toolStripStatusLabel1.Text = currentFile + " is Opened...";
            }
            else
            {
                for (int x = 0; x < rowSize; x++)
                {
                    myArray[x, 0] = String.Empty;
                    myArray[x, 1] = String.Empty;
                    myArray[x, 2] = String.Empty;
                    myArray[x, 3] = String.Empty;
                }
                toolStripStatusLabel1.Text = "!Application  is Loaded with Empty Data...";
            }
            DisplayArray();
        }

        private void buttonAutoLoadData_Click(object sender, EventArgs e)
        {
            myArray[0, 0] = "Array"; myArray[0, 1] = "Array"; myArray[0, 2] = "Linear"; myArray[0, 3] = "An array is a collection of elements of the same type placed in contiguous memory locations that can be individually referenced by using an index to a unique identifier.";
            myArray[1, 0] = "Two Dimension Array"; myArray[1, 1] = "Array"; myArray[1, 2] = "Linear"; myArray[1, 3] = "Arrays can have more than one dimension.The multidimensional array can be declared by adding commas in the square brackets. For example, [,] declares two-dimensional array";
            myArray[2, 0] = "List"; myArray[2, 1] = "List"; myArray[2, 2] = "Linear"; myArray[2, 3] = "The List<T> is a collection of strongly typed objects that can be accessed by index and having methods for sorting, searching, and modifying list. It is the generic version of the ArrayList that comes under System.Collection.Generic namespace.";
            myArray[3, 0] = "Linked list"; myArray[3, 1] = "List"; myArray[3, 2] = "Linear"; myArray[3, 3] = "A LinkedList is a linear data structure which stores element in the non-contiguous location. The elements in a linked list are linked with each other using pointers. Or in other words, LinkedList consists of nodes where each node contains a data field and a reference(link) to the next node in the list.";
            myArray[4, 0] = "Self - Balance Tree"; myArray[4, 1] = "Tree"; myArray[4, 2] = "Non-Linear"; myArray[4, 3] = " A self-balancing binary search tree (BST) is any node-based binary search tree that automatically keeps its height (maximal number of levels below the root) small in the face of arbitrary item insertions and deletions.";
            myArray[5, 0] = "Heaps"; myArray[5, 1] = "Tree"; myArray[5, 2] = "Non-Linear"; myArray[5, 3] = "A Heap is a special Tree-based data structure in which the tree is a complete binary tree. Generally, Heaps can be of two types: Max-Heap: In a Max-Heap the key present at the root node must be greatest among the keys present at all of it's children.";
            myArray[6, 0] = "Binary Search Tree"; myArray[6, 1] = "Tree"; myArray[6, 2] = "Non-Linear"; myArray[6, 3] = "A binary search tree (BST) is a binary tree where each node has a Comparable key (and an associated value) and satisfies the restriction that the key in any node is larger than the keys in all nodes in that node's left subtree and smaller than the keys in all nodes in that node's right subtree.";
            myArray[7, 0] = "Graph"; myArray[7, 1] = "Graphs"; myArray[7, 2] = "Non-Linear"; myArray[7, 3] = "A Graph is a non-linear data structure consisting of nodes and edges. The nodes are sometimes also referred to as vertices and the edges are lines or arcs that connect any two nodes in the graph";
            myArray[8, 0] = "Set"; myArray[8, 1] = "Abstract"; myArray[8, 2] = "Non-Linear"; myArray[8, 3] = "A Graph is a non-linear data structure consisting of nodes and edges. The nodes are sometimes also referred to as vertices and the edges are lines or arcs that connect any two nodes in the graph";
            myArray[9, 0] = "Queue"; myArray[9, 1] = "Abstract"; myArray[9, 2] = "Linear"; myArray[9, 3] = "Queue is a special type of collection that stores the elements in FIFO style (First In First Out), exactly opposite of the Stack<T> collection. It contains the elements in the order they were added. C# includes generic Queue<T> and non-generic Queue collection. It is recommended to use the generic Queue<T> collection.";
            myArray[10, 0] = "Stack"; myArray[10, 1] = "Abstract"; myArray[10, 2] = "Linear"; myArray[10, 3] = "Stack is a special type of collection that stores elements in LIFO style (Last In First Out). C# includes the generic Stack<T> and non-generic Stack collection classes. It is recommended to use the generic Stack<T> collection.Stack is useful to store temporary data in LIFO style, and you might want to delete an element after retrieving its value.";
            myArray[11, 0] = "Hash Table"; myArray[11, 1] = "Hash"; myArray[11, 2] = "Non-Linear"; myArray[11, 3] = "Stack is a special type of collection that stores elements in LIFO style (Last In First Out). C# includes the generic Stack<T> and non-generic Stack collection classes. It is recommended to use the generic Stack<T> collection.Stack is useful to store temporary data in LIFO style, and you might want to delete an element after retrieving its value.";
            DisplayArray();
            toolStripStatusLabel1.Text = "DATA IS AUTO-LOADED";
        }
        #endregion

        #region LISTBOX DISPLAY 

      //  8.7	Create a method so the user can select a definition(Name) from the Listbox and all the information is displayed in the appropriate Textboxes,
        private void listBoxWki_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxWiki.SelectedIndex == -1 && listBoxCategory.SelectedIndex == -1)
            {
                toolStripStatusLabel1.Text = "!Element in the List Not selected";
                return;
            }

            if (listBoxWiki.SelectedIndex != -1)
            {
                listBoxCategory.ClearSelected();
                int currentIndex = listBoxWiki.SelectedIndex;
                textBoxName.Text = myArray[currentIndex, 0];
                toolStripStatusLabel1.Text = String.Format("\"{0}\" is Selected from Row {1} ", textBoxName.Text, currentIndex + 1);
                textBoxCategory.Text = myArray[currentIndex, 1];
                textBoxStructure.Text = myArray[currentIndex, 2];
                textBoxDefinition.Text = myArray[currentIndex, 3];
            }
        }

        private void listBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if nothing is selected from Name and Category then execute this 
            if (listBoxWiki.SelectedIndex == -1 && listBoxCategory.SelectedIndex == -1)
            {
                toolStripStatusLabel1.Text = "!Element in the List Not selected";
                return;
            }
            //when anything selected from Name listbox, it will show in the corresponding textbox.
            if (listBoxCategory.SelectedIndex != -1)
            {
                listBoxWiki.ClearSelected();
                int currentIndex = listBoxCategory.SelectedIndex;
                textBoxName.Text = myArray[currentIndex, 0];
                toolStripStatusLabel1.Text = String.Format("\"{0}\" is Selected from Row {1} ", textBoxName.Text, currentIndex + 1);
                textBoxCategory.Text = myArray[currentIndex, 1];
                textBoxStructure.Text = myArray[currentIndex, 2];
                textBoxDefinition.Text = myArray[currentIndex, 3];
            }

        }
        #endregion

        #region FILE HANDLING

      //  8.8	Create a SAVE button so the information from the 2D array can be written into a binary file called definitions.dat which is sorted by Name,
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenBinary = new OpenFileDialog();
            OpenBinary.InitialDirectory = defaultFileName;
            //defaultFileName = OpenBinary.FileName;
            OpenBinary.Filter = "BIN Files|*.bin";
            OpenBinary.Title = "Select a BIN File";

            if (OpenBinary.ShowDialog() == DialogResult.OK)
            {
                currentFile = OpenBinary.FileName;
                openRecord(currentFile);
                toolStripStatusLabel1.Text = currentFile + " is Opened...";
            }
            DisplayArray();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveBinary = new SaveFileDialog();
            saveBinary.InitialDirectory = defaultFileName;
            saveBinary.Filter = "binary files (*.bin)|*.bin|All files (*.*)|*.*";            //setting so that user can save as binary files
            saveBinary.DefaultExt = "bin";                                                   //save the file as bin extension if all files is choosed.
                                                                                             //saveBinary.FileName = Path.GetFileName(currentFile);                             //set filename in the SaveFileDialog // getFileName extract the name only without path.

            DialogResult sr = saveBinary.ShowDialog();

            if (saveBinary.FileName != "")
            {
                saveRecord(saveBinary.FileName);
            }
            else
            {
                saveRecord(defaultFileName);
            }

        }

        private void openRecord(string openFileName)
        {

            try
            {
                using (Stream stream = File.Open(openFileName, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();

                    for (int y = 0; y < colSize; y++)
                    {
                        for (int x = 0; x < rowSize; x++)
                        {
                            myArray[x, y] = (string)bin.Deserialize(stream);
                        }
                    }
                }
            }
            catch (SerializationException)
            {
                for (int x = 0; x < rowSize; x++)
                {
                    myArray[x, 0] = String.Empty;
                    myArray[x, 1] = String.Empty;
                    myArray[x, 2] = String.Empty;
                    myArray[x, 3] = String.Empty;
                }
                toolStripStatusLabel1.Text = "!File is Loaded with Empty Data...";
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            DisplayArray();
        }

        private void saveRecord(string saveFileName)
        {
            try
            {
                using (Stream stream = File.Open(saveFileName, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    for (int y = 0; y < colSize; y++)
                    {
                        for (int x = 0; x < rowSize; x++)
                        {
                            bin.Serialize(stream, myArray[x, y]);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region ADDFUNCTIONS

        //8.2	Create an ADD button that will store the information from the 4 text boxes into the 2D array,
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // only add when all the textbox is not null or empty
            if (!string.IsNullOrEmpty(textBoxName.Text) && !string.IsNullOrEmpty(textBoxCategory.Text) && !string.IsNullOrEmpty(textBoxStructure.Text) && !string.IsNullOrEmpty(textBoxDefinition.Text))
            {

                for (int x = 0; x < rowSize; x++)
                {
                    //if row is 11 and either of them is not empty and not - (which means there is data) it will return and display the message.
                    // need - as we will replacing the deleted item with - (logic that user can also fill the deleted item with text)
                    if (x == 11 && (myArray[x, 0] != String.Empty && myArray[x, 0] != "-"))
                    {
                        MessageBox.Show("CANNOT EXCEED MORE THAN 12 ROWS", "*User Information*", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearTextBox();
                        return;
                    }

                    // if the element has empty and - then it will add from textbox
                    if (myArray[x, 0] == String.Empty || myArray[x, 0] == "-")
                    {
                        myArray[x, 0] = textBoxName.Text;
                        myArray[x, 1] = textBoxCategory.Text;
                        myArray[x, 2] = textBoxStructure.Text;
                        myArray[x, 3] = textBoxDefinition.Text;
                        var result = MessageBox.Show("Proceed with new Record?", "Add New Record",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.OK)
                        {
                            toolStripStatusLabel1.Text = String.Format("\"{0}\" is Added to Row {1}... ", textBoxName.Text, x + 1);
                            break;
                        }
                        // if user cancel to add, then fill the every element with empty string, to prevent the null value.
                        else
                        {
                            myArray[x, 0] = String.Empty;
                            myArray[x, 1] = String.Empty;
                            myArray[x, 2] = String.Empty;
                            myArray[x, 3] = String.Empty;
                            toolStripStatusLabel1.Text = "!User has Canceled the Add Operation...";
                            break;
                        }
                    }

                }
                clearTextBox();
                DisplayArray();
            }
            else
            {
                // will set the error message, inform the user the data is required or mandatory need to fill the textbox.
                if (string.IsNullOrEmpty(textBoxName.Text))
                {
                    errorProvider1.SetError(textBoxName, "Data Required...");

                }
                if (string.IsNullOrEmpty(textBoxCategory.Text))
                {
                    errorProvider1.SetError(textBoxCategory, "Data Required...");

                }
                if (string.IsNullOrEmpty(textBoxStructure.Text))
                {
                    errorProvider1.SetError(textBoxStructure, "Data Required...");

                }
                if (string.IsNullOrEmpty(textBoxDefinition.Text))
                {
                    errorProvider1.SetError(textBoxDefinition, "Data Required...");

                }
                toolStripStatusLabel1.Text = "!Data required to be filled in all the boxes...";
            }
        }
        #endregion

        #region DELETE, EDIT DATA
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxWiki.SelectedIndex == -1)
            {
                toolStripStatusLabel1.Text = "!Element in the List Not selected";
                return;
            }

            int currentIndex = listBoxWiki.SelectedIndex;
            DialogResult modifyTask = MessageBox.Show("Data will Permanently Deleted,Do you want to Continue?", "Delete the data...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (modifyTask == DialogResult.Yes)
            {
                //after deleteting replace the element with -
                myArray[currentIndex, 0] = "-";
                myArray[currentIndex, 1] = "-";
                myArray[currentIndex, 2] = "-";
                myArray[currentIndex, 3] = "-";
                toolStripStatusLabel1.Text = "!Sucessfully Deleted the Data...";
                DisplayArray();
            }
            else
            {
                toolStripStatusLabel1.Text = "!User had cancelled the Delete Operation..";
            }
            clearTextBox();

        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listBoxWiki.SelectedIndex == -1)
            {
                toolStripStatusLabel1.Text = "!Element in the List Not selected";
                return;
            }

            int currentIndex = listBoxWiki.SelectedIndex;
            String oldName = myArray[currentIndex, 0];
            String oldCategory = myArray[currentIndex, 1];
            String oldStructure = myArray[currentIndex, 2];
            String oldDefinition = myArray[currentIndex, 3];

            if (oldName == textBoxName.Text && oldCategory == textBoxCategory.Text && oldStructure == textBoxStructure.Text && oldDefinition == textBoxDefinition.Text)
            {
                toolStripStatusLabel1.Text = "Not a Single  Element is changed";
                return;
            }
            else
            {
                DialogResult modifyTask = MessageBox.Show("Data will Permanently Edited,Do you want to Continue?", "Edit the data...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (modifyTask == DialogResult.Yes)
                {
                    myArray[currentIndex, 0] = textBoxName.Text;
                    myArray[currentIndex, 1] = textBoxCategory.Text;
                    myArray[currentIndex, 2] = textBoxStructure.Text;
                    myArray[currentIndex, 3] = textBoxDefinition.Text;
                    DisplayArray();
                }
                else
                {
                    toolStripStatusLabel1.Text = "User had cancelled to modify";
                }
            }
            clearTextBox();
        }
        #endregion

        #region SORT AND SEARCH

     //   8.4	Write the code for a Bubble Sort method to sort the 2D array by Name ascending.
     //   Ensure you use a separate swap method that passes(by reference) the array element to be swapped(do not use any built-in array methods),
        private void sortByColumn(int sortedColumn)
        {
            //parametised with sortedColumn so that we can use the method to sort by any column when required.
            int num = sortedColumn;
            for (int i = 0; i < rowSize -1; i++)
            {
                for (int rows = 0; rows < rowSize -i - 1; rows++)
                {
                    // getting null or empty value means there is no further data, so break from loop.
                    if (string.IsNullOrEmpty(myArray[rows, num]) || string.IsNullOrEmpty(myArray[rows + 1, num]))
                    {
                        break;
                    }
                    // to sort - (logic that if other element is - then dont excute the below line and go to another rows)
                    if (myArray[rows + 1, num].Equals("-"))
                    {
                        continue;
                    }

                    // now after above - we continue and now [row,num] is - and another element is not - then do swap. this will take - to the end of the array.
                    if ((myArray[rows, num].CompareTo(myArray[rows + 1, num]) > 0) || myArray[rows, num].Equals("-") && !myArray[rows + 1, num].Equals("-"))
                    {
                        swap(num, rows, myArray);
                    }
                }
            }

        }

        private void swap(int columnToBeSorted, int rows, string[,] twoDArray)
        {
            string tempForColumn = twoDArray[rows, columnToBeSorted];
            twoDArray[rows, columnToBeSorted] = twoDArray[rows + 1, columnToBeSorted];
            twoDArray[rows + 1, columnToBeSorted] = tempForColumn;
            //this will sort the other rows according to the sorted column
            for (int column = 0; column < colSize; column++)
            {
               // skip the sorted column that we choosed to be sorted
                if(column == columnToBeSorted)
                {
                    continue;
                }
                string tempForRow = twoDArray[rows, column];
                twoDArray[rows, column] = twoDArray[rows + 1, column];
                twoDArray[rows + 1, column] = tempForRow;
            }
        }

        //label created so that we can click on the label to sort the listbox
        private void labelName_Click(object sender, EventArgs e)
        {
            sortByColumn(0);
            DisplayArray();
            toolStripStatusLabel1.Text = "Data Sorted By Name...";
        }

        private void labelCategory_Click(object sender, EventArgs e)
        {
            sortByColumn(1);
            DisplayArray();
            toolStripStatusLabel1.Text = "Data Sorted By Category...";
        }

       // 8.5	Write the code for a Binary Search for the Name in the 2D array and display the information in the other textboxes when found
       // add suitable feedback if the search in not successful and clear the search textbox(do not use any built-in array methods),
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            sortByColumn(0);
            DisplayArray();
            string target = textBoxSearch.Text.ToUpper();
            //calling binarySearch method which return tuple with the value of mid and found.
            var (mid, found) = binarySearch(target);

            if (found)
            {
                listBoxWiki.SelectedIndex = mid;
                toolStripStatusLabel1.Text = target + " is found in line " + (mid + 1);
                textBoxSearch.Clear();
                textBoxSearch.Focus();
            }

            else
            {
                toolStripStatusLabel1.Text = "!Could not found the typed word...";
                textBoxSearch.Clear();
                textBoxSearch.Focus();
            }

        }

        //method returns tuple which contains the value of mid and found
        private (int, bool) binarySearch(string target)
        {
            int min = 0;
            int rowMax = rowSize - 1;
            int mid = 0;
            bool found = false;
            while (min <= rowMax)
            {
                mid = (min + rowMax) / 2;
                if (myArray[mid, 0].ToUpper().Equals(target))
                {
                    found = true;
                    break;
                }
                else if (target.CompareTo(myArray[mid, 0].ToUpper()) < 0)
                {
                    rowMax = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return (mid, found);
        }
        #endregion

        #region UTILITIES
        //when the textbox is changed in the textbox then clear the error, this is implemented for Add function
        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void textBoxCategory_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void textBoxStructure_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        private void textBoxDefinition_TextChanged(object sender, EventArgs e)
        {
            errorProvider1.Clear();
        }

        // to clear the prvious user feedback.
        private void IntraWiki_MouseDown(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "!Application is Running...";
        }

        //8.3	Create a CLEAR method to clear the four text boxes so a new definition can be added,
        private void clearTextBox()
        {
            textBoxName.Clear();
            textBoxCategory.Clear();
            textBoxStructure.Clear();
            textBoxDefinition.Clear();
            textBoxName.Focus();
        }
        #endregion

      
    }
}
