#include "windows.h"
#include <iostream>
#include <fstream> 
#include <string>
#include <sstream>  
#include <chrono>
#include <tchar.h>
using namespace std;

typedef int T;  // тип элементов
typedef int hashTableIndex;// индекс в хеш-таблице
int hashTableSize;
T* hashTable;
bool* used;

hashTableIndex myhash(T data);
void insertData(T data);
void deleteData(T data);
bool findData(T data);
int dist(hashTableIndex a, hashTableIndex b);

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

int _tmain(int argc, _TCHAR* argv[]) {

    int vals = 100;// значения менятются от 100 до 10к
    for (int i = 0; i < 100; i++) {
        DataGenerator(vals);
        vals += 100;
    }
    ifstream reader("inPut.txt");
    //ofstream timer("time.txt");
    //ofstream size("size.txt");
    //ofstream search("search.txt");
    //ofstream insert("insert.txt");
    ofstream del("del.txt");
    string str;

    //чтение
    while (getline(reader, str)) {
        int arsize;
        reader >> arsize; //там сначала размер массива
        int* arr = new int[arsize]();
        istringstream ss(str);
        for (int i = 0; i < arsize; i++)
            ss >> arr[i];


        //
        //
        int i;
        int maxnum = arsize;
        hashTableSize = arsize;
        hashTable = new T[hashTableSize];

        used = new bool[hashTableSize];
        for (i = 0; i < hashTableSize; i++) {
            hashTable[i] = 0;
            used[i] = false;
        }


        // генерация массива
        //for (i = 0; i < maxnum; i++)
        //    a[i] = rand();
        // 

        auto begin = std::chrono::steady_clock::now();
        // заполнение хеш-таблицы элементами массива
        //for (i = 0; i < maxnum; i++) insertData(arr[i]);

        // поиск элементов массива по хеш-таблице
        //for (i = maxnum - 1; i >= 0; i--) findData(arr[i]);

        // вывод элементов массива в файл List.txt
        /*ofstream out("List.txt");
        for (i = 0; i < maxnum; i++) {
            out << arr[i];
            if (i < maxnum - 1) out << "\t";
        }
        out.close();*/

        // сохранение хеш-таблицы в файл HashTable.txt
        //out.open("HashTable.txt");
        /*for (i = 0; i < hashTableSize; i++) {
            out << i << "  :  " << used[i] << " : " << hashTable[i] << endl;
        }*/
        //out.close();

        // очистка хеш-таблицы
        for (i = maxnum - 1; i >= 0; i--) {
            deleteData(arr[i]);
        }
        //
        //

        auto end = std::chrono::steady_clock::now();
        auto elapsed_mics = std::chrono::duration_cast<std::chrono::microseconds>(end - begin);

        del << elapsed_mics.count() << endl;
        //size << arsize << endl;
    }
    cout << "end" << endl;
    return 0;
}

// хеш-функция размещения величины
hashTableIndex myhash(T data) {
    return (data % hashTableSize);
}

// функция поиска местоположения и вставки величины в таблицу
void insertData(T data) {
    hashTableIndex bucket;
    bucket = myhash(data);
    while (used[bucket] && hashTable[bucket] != data)
        bucket = (bucket + 1) % hashTableSize;
    if (!used[bucket]) {
        used[bucket] = true;
        hashTable[bucket] = data;
    }
}

// функция поиска величины, равной data
bool findData(T data) {
    hashTableIndex bucket;
    bucket = myhash(data);
    while (used[bucket] && hashTable[bucket] != data)
        bucket = (bucket + 1) % hashTableSize;
    return used[bucket] && hashTable[bucket] == data;
}

//функция удаления величины из таблицы
void deleteData(T data) {
    int bucket, gap;
    bucket = myhash(data);
    while (used[bucket] && hashTable[bucket] != data)
        bucket = (bucket + 1) % hashTableSize;
    if (used[bucket] && hashTable[bucket] == data) {
        used[bucket] = false;
        gap = bucket;
        bucket = (bucket + 1) % hashTableSize;
        while (used[bucket]) {
            if (bucket == myhash(hashTable[bucket]))
                bucket = (bucket + 1) % hashTableSize;
            else if (dist(myhash(hashTable[bucket]), bucket) < dist(gap, bucket))
                bucket = (bucket + 1) % hashTableSize;
            else {
                used[gap] = true;
                hashTable[gap] = hashTable[bucket];
                used[bucket] = false;
                gap = bucket;
                bucket++;
            }
        }
    }
}

// функция вычисления расстояние от a до b (по часовой стрелке, слева направо) 
int dist(hashTableIndex a, hashTableIndex b) {
    return (b - a + hashTableSize) % hashTableSize;
}