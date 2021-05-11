#include "windows.h"
#include <iostream>
#include <fstream> 
#include <string>
#include <sstream>  
#include <chrono>
using namespace std;


int updateGap(int gap)
{
    gap = (gap * 10) / 13;
    if (gap < 1)
        return 1;
    else
        return gap;
}

void combSort(int arr[], int n, int& counter)
{
    int gap = n;
    bool swapped = true;
    while (gap > 1 || swapped == true)
    {
        gap = updateGap(gap);
        swapped = false;
        for (int i = 0; i < (n - gap); i++)
        {
            int temp;
            if (arr[i] > arr[i + gap])
            {
                temp = arr[i];
                arr[i] = arr[i + gap];
                arr[i + gap] = temp;
                counter++;
                swapped = true;
            }
        }
    }
}

int DataGenerator(int& val) 
{
    ofstream write;
    write.open("inPut.txt", ofstream::app);
    srand(time(NULL));
    int a = val;
    write << a << " ";
    for (int i = 0; i < a; i++)
        write << rand() % 100 << " ";
    Sleep(2000);
    write << endl;
    return 0;
}

int main() 
{
    //ofstream запись
    //ifstream чтение
    //setlocale(LC_ALL, "ru");
    int vals = 100;
    for (int i = 0; i < 100; i++) {
        DataGenerator(vals);
        vals += 100;
    }

    ifstream reader("inPut.txt");
    ofstream timer("time.txt");
    ofstream iterations("iterations.txt");
    ofstream size("size.txt");
    string str;

    while (getline(reader, str)) {
        int arsize;
        reader >> arsize;
        int* arr = new int[arsize]();
        istringstream ss(str);
        for (int i = 0; i < arsize; i++)
            ss >> arr[i];

        int counter = 0;
        auto begin = std::chrono::steady_clock::now();

        combSort(arr, arsize, counter);

        auto end = std::chrono::steady_clock::now();
        auto elapsed_mics = std::chrono::duration_cast<std::chrono::microseconds>(end - begin);

        timer << elapsed_mics.count() << endl;
        iterations << counter << endl;
        size << arsize << endl;
    }
    return 0;

    /*string path = "myInput.txt";
    ofstream fout;

    fout.open(path);

    if (!fout.is_open()) {
        cout << "Ошибка открытия файла" << endl;
    }
    else {
        fout << "dadas";
    }

    fout.close();*/
}