#include <iostream>
#include "windows.h"
#include <fstream> 
#include <string>
#include <sstream>  
#include <chrono>
#include <tchar.h>
using namespace std;

typedef int T;  // òèï ýëåìåíòîâ
typedef int splayTreeIndex;// èíäåêñ â õåø-òàáëèöå
T* splayTree;
bool* used;

// Узел АВЛ-дерева
class node
{
public:
    int key;
    node* left, * right;
};

/* Вспомогательная функция, которая выделяет
новый узел с заданным key и left и right, указывающими в NULL. */
node* newNode(int key)
{
    node* Node = new node();
    Node->key = key;
    Node->left = Node->right = NULL;
    return (Node);
}

// Служебная функция для разворота поддерева с корнем y вправо.
// Смотрите диаграмму, приведенную выше.
node* rightRotate(node* x)
{
    node* y = x->left;
    x->left = y->right;
    y->right = x;
    return y;
}

// Служебная функция для разворота поддерева с корнем x влево 
// Смотрите диаграмму, приведенную выше. 
node* leftRotate(node* x)
{
    node* y = x->right;
    x->right = y->left;
    y->left = x;
    return y;
}

// Эта функция поднимет ключ
// в корень, если он присутствует в дереве. 
// Если такой ключ отсутствует в дереве, она
// поднимет в корень самый последний элемент,
// к которому был осуществлен доступ.
// Эта функция изменяет дерево
// и возвращает новый корень (root).
node* splay(node* root, int key)
{
    // Базовые случаи: root равен NULL или
    // ключ находится в корне
    if (root == NULL || root->key == key)
        return root;

    // Ключ лежит в левом поддереве
    if (root->key > key)
    {
        // Ключа нет в дереве, мы закончили
        if (root->left == NULL) return root;

        // Zig-Zig (Левый-левый)
        if (root->left->key > key)
        {
            // Сначала рекурсивно поднимем
             // ключ в качестве корня left-left
            root->left->left = splay(root->left->left, key);

            // Первый разворот для root, 
             // второй разворот выполняется после else 
            root = rightRotate(root);
        }
        else if (root->left->key < key) // Zig-Zag (Left Right) 
        {
            // Сначала рекурсивно поднимаем
             // ключ в качестве кореня left-right
            root->left->right = splay(root->left->right, key);

            // Выполняем первый разворот для root->left
            if (root->left->right != NULL)
                root->left = leftRotate(root->left);
        }

        // Выполняем второй разворот для корня
        return (root->left == NULL) ? root : rightRotate(root);
    }
    else // Ключ находится в правом поддереве
    {
        // Ключа нет в дереве, мы закончили
        if (root->right == NULL) return root;

        // Zag-Zig (Правый-левый)
        if (root->right->key > key)
        {
            // Поднять ключ в качестве кореня right-left
            root->right->left = splay(root->right->left, key);

            // Выполняем первый поворот для root->right
            if (root->right->left != NULL)
                root->right = rightRotate(root->right);
        }
        else if (root->right->key < key)// Zag-Zag (Правый-правый) 
        {
            // Поднимаем ключ в качестве корня 
             // right-right и выполняем первый разворот
            root->right->right = splay(root->right->right, key);
            root = leftRotate(root);
        }

        // Выполняем второй разворот для root
        return (root->right == NULL) ? root : leftRotate(root);
    }
}

// Функция для вставки нового ключа k в splay-дерево с заданным корнем
node* insert(node* root, int k)
{
    // Простой случай: если дерево пусто
    if (root == NULL) return newNode(k);

    // Делаем ближайший узел-лист корнем 
    root = splay(root, k);

    // Если ключ уже существует, то возвращаем его
    if (root->key == k) return root;

    // В противном случае выделяем память под новый узел
    node* newnode = newNode(k);

    // Если корневой ключ больше, делаем корень правым дочерним элементом нового узла, копируем левый дочерний элемент корня в качестве левого дочернего элемента нового узла
    if (root->key > k)
    {
        newnode->right = root;
        newnode->left = root->left;
        root->left = NULL;
    }

    // Если корневой ключ меньше, делаем корень левым дочерним элементом нового узла, копируем правый дочерний элемент корня в качестве правого дочернего элемента нового узла
    else
    {
        newnode->left = root;
        newnode->right = root->right;
        root->right = NULL;
    }

    return newnode; // новый узел становится новым корнем
}

// Служебная функция для вывода 
// обхода в дерева ширину. 
// Функция также выводит высоту каждого узла
void preOrder(node* root)
{
    if (root != NULL)
    {
        cout << root->key << " ";
        preOrder(root->left);
        preOrder(root->right);
    }
}

node* search(node* root, int key)
{
    return splay(root, key);
}

struct node* delete_key(struct node* root, int key)
{
    struct node* temp;
    if (!root)
        return NULL;

    // Splay the given key    
    root = splay(root, key);

    // If key is not present, then
    // return root
    if (key != root->key)
        return root;

    // If key is present
    // If left child of root does not exist
    // make root->right as root   
    if (!root->left)
    {
        temp = root;
        root = root->right;
    }

    // Else if left child exits
    else
    {
        temp = root;

        /*Note: Since key == root->key,
        so after Splay(key, root->lchild),
        the tree we get will have no right child tree
        and maximum node in left subtree will get splayed*/
        // New root
        root = splay(root->left, key);

        // Make right child of previous root  as
        // new root's right child
        root->right = temp->right;
    }

    // free the previous root node, that is,
    // the node containing the key
    free(temp);

    // return root of the new Splay Tree
    return root;

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

int _tmain(int argc, _TCHAR* argv[]) {

    int vals = 100;
    for (int i = 0; i < 100; i++) {
        DataGenerator(vals);
        vals += 100;
    }
    ifstream reader("inPut.txt");

    //ofstream size("size.txt");
    ofstream insert("insert.txt");
    string str;
    while (getline(reader, str)) {
        int arsize;
        reader >> arsize;
        int* arr = new int[arsize]();
        istringstream ss(str);
        for (int i = 0; i < arsize; i++)
            ss >> arr[i];

        int i;
        int maxnum = arsize;

        int splaySize = arsize;
        node* splayTree = new node();

        splayTree = newNode(1);
        auto begin = std::chrono::steady_clock::now();

        for (i = 1; i < maxnum; i++) {
            splay(splayTree, arr[i]);
        }

        auto end = std::chrono::steady_clock::now();
        auto elapsed_mics = std::chrono::duration_cast<std::chrono::microseconds>(end - begin);

        insert << elapsed_mics.count() << endl;
        //size << arsize << endl;
    }
    cout << "end" << endl;
    return 0;
}