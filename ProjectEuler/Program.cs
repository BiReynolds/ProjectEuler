﻿using System.Diagnostics;
using System.Numerics;

public class ProjectEuler {
    public static Stopwatch stopwatch = new Stopwatch();
    public static Problem? currProblem;
    public static void Main(string[] args) {
        Console.WriteLine("Which Project Euler problem would you like to see? (1-50)");
        string? problemNum = Console.ReadLine();
        try {
            // Disable a couple of warnings related to null references, since we are in a try statement
            #pragma warning disable CS8604 // Possible null reference argument.
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            // Fetching the corresponding problem
            string problemString = "Problem"+problemNum;
            Type? bleh = Type.GetType(problemString, true);
            currProblem = (Problem?)Activator.CreateInstance(bleh);
            // Showing problem and solution, including the time taken for the program to find the solution
            currProblem.ShowProblemStatement();
            Console.WriteLine("Answer: ");
            stopwatch.Start();
            currProblem.ShowSolution();
            stopwatch.Stop();
            Console.WriteLine($"Answer found in {stopwatch.ElapsedMilliseconds} milliseconds!");
            Console.WriteLine("Press a to see another problem, or any other key to exit program.");
            char pressedKey = Console.ReadKey().KeyChar;
            if (pressedKey == 'a') {
                stopwatch.Reset();
                Console.WriteLine();
                Main(args);
            }
            //Restore warnings
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
            #pragma warning restore CS8604 // Possible null reference argument.
        } catch {
            Console.WriteLine("Invalid problem number. Please ensure your entry is a positive integer between 1 and 50");
            Main(args);
        }
    }
}

public abstract class Problem {
    public abstract void ShowProblemStatement();
    public abstract void ShowSolution();
}

public class Problem1 : Problem
{
    public override void ShowProblemStatement()
    {
        Console.WriteLine("If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3,5,6, and 9.  The sum of these multiples is 23.");
        Console.WriteLine("Find the sum of all the multiples of 3 or 5 below 1,000.");
    }

    public override void ShowSolution()
    {
        int result = 0;
        for (int i = 0; i < 1000; i++){
            if (i%3 == 0) {
                result += i;
            } else if (i%5 == 0) {
                result += i;
            }
        }
        Console.WriteLine(result);
    }
}

public class Problem2 : Problem
{
    public override void ShowProblemStatement()
    {
        Console.WriteLine("Each new term in the Fibonacci sequence is generated by adding the previous two terms. By starting with 1 and 2, the first 10 terms will be:");
        Console.WriteLine("1, 2, 3, 5, 8, 13, 21, 34, 55, 89,...");
        Console.WriteLine("By considering the terms in the Fibonacci sequence whose values do not exceed four million, find the sum of the even-valued terms.");
    }

    public override void ShowSolution()
    {
        int result = 0;

        int i1 = 1;
        int i2 = 2; 
        int temp;
        while (i2 <= 4000000) {
            if (i2 % 2 == 0) {
                result += i2;
            }
            temp = i1;
            i1=i2;
            i2 = temp+i1;
        }
        Console.WriteLine(result);
    }
}

public class Problem3 : Problem
{
    public override void ShowProblemStatement()
    {
        Console.WriteLine("The prime factors of 13195 are 5, 7, 13, and 29.");
        Console.WriteLine("What is the largest prime factor of the number 600851475143");
    }

    public override void ShowSolution()
    {
        Console.WriteLine(GetLargestPrimeFactor(600851475143));
    }

    public long GetLargestPrimeFactor(long N) {
        for (int i=2; i*i <= N; i++) {
            if (N % i == 0) {
                return GetLargestPrimeFactor(N/i);
            }
        }
        return N;
    }
}

public class Problem4 : Problem
{
    public override void ShowProblemStatement()
    {
        Console.WriteLine("A palindromic number reads the same both ways. The largest palindrome made from the product of two 2-digit numbers is 9009 = 91*99.");
        Console.WriteLine("Find the largest palindrome made from the product of two 3-digit numbers");
    }

    public override void ShowSolution()
    {
        // Any product of two 3-digit numbers is going to have at MOST 6 digits.  We will assume the LARGEST such palindrome has 6 digits (mainly because it worked; we could always
        // use the same idea as below while assuming that the largest palindrome has 5 digits if this returned no result).  
        // The method is this: given a palindrome, we check if it's possible to write it as a product of 3-digit numbers.  We start with the largest 6 digit palindrome (999999) and
        // work our way down until we find one.  
        int a = 9;
        int b = 9;
        int c = 9;
        int palindrome;
        while (a > 0) {
            b = 9;
            while (b >= 0) {
                c = 9;
                while (c >= 0) {
                    palindrome = 100001*a + 10010*b + 1100*c;
                    if (IsProdOf3DigitNumbers(palindrome)) {
                        Console.WriteLine(palindrome);
                        return;
                    } else {
                        c -= 1;
                    }
                }
                b -= 1;
            }
            a -= 1;
        }
    }

    public bool IsProdOf3DigitNumbers(int N) {
        for (int i=Math.Max(100,N/999); i<Math.Min(999,N/100); i++) {
            if (N%i == 0) {
                Console.WriteLine($"{N} is the product of {i} and {N/i}.");
                return true;
            }
        }
        return false;
    }
}

public class Problem5 : Problem
{
    public override void ShowProblemStatement()
    {
        Console.WriteLine("2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.");
        Console.WriteLine("What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?");
    }

    public override void ShowSolution()
    {
        // As with many divisibility questions, we can restrict our attention to primes for the most part.  The trick here is that we need to catch the HIGHEST POWER of each prime
        // in the range to ensure that the number is divisible by ALL of the entries, and just take the product.  Given the small range, we could cheat by just knowing all the 
        // relevant primes... BUT we'll do it proper

        int result = 1;
        for (int i=2; i<=20; i++) {
            if (IsPrime(i)) {
                int mult = (int)Math.Pow(i,(int)Math.Log(20,i));
                result *= mult;
            }
        }
        Console.WriteLine(result);
    }

    public bool IsPrime(int N) {
        for (int i=2; i*i <= N; i++) {
            if (N%i == 0) {
                return false;
            }
        }
        return true;
    }
}

public class Problem6 : Problem
{
    public override void ShowProblemStatement()
    {
        Console.WriteLine("The sum of the squares of the first ten natural numbers is 1^2 + 2^2 + ... + 10^2 = 385.");
        Console.WriteLine("The SQUARE of the SUM of the first ten natural numbers is (1 + 2 + ... + 10)^2 = 3025");
        Console.WriteLine("Hence the difference between the sum of the squares of the first ten natural numbers and the square of the sum is 2640");
        Console.WriteLine("Find the difference between the sum of the square of the first one hundred natural numbers and the square of the sum.");
    }

    public override void ShowSolution()
    {
        // We calculate two separate sums: An is the sum of the first n natural numbers (not squared), and Bn is the sum of the squares of the first n natural numbers.  
        //Of course, our result is then An^2 - Bn
        int An = 0;
        int Bn = 0;
        for (int i=1; i <= 100; i++) {
            An += i;
            Bn += i*i;
        }
        Console.WriteLine(An*An - Bn);
    }
}

public class Problem7 : Problem
{
    public override void ShowProblemStatement()
    {
        Console.WriteLine("By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.");
        Console.WriteLine("What is the 10,001st prime number?");
    }

    public override void ShowSolution()
    {
        List<int> primes = new List<int>(10001);
        int n = 2;
        bool isPrime;
        while (primes.Count != 10001) {
            isPrime = true;
            foreach (int prime in primes) {
                if (n % prime == 0) {
                    isPrime = false;
                    break;
                }
            }
            if (isPrime) {
                primes.Add(n);
            }
            n += 1;
        }
        Console.WriteLine(n-1);
    }
}

public class Problem8 : Problem
{
    public override void ShowProblemStatement()
    {
        Console.WriteLine("The four adjacent digits in the 1000-digit number below that have the greatest product are 9*9*8*9 = 5832.");
        Console.WriteLine(@"
                        73167176531330624919225119674426574742355349194934
                        96983520312774506326239578318016984801869478851843
                        85861560789112949495459501737958331952853208805511
                        12540698747158523863050715693290963295227443043557
                        66896648950445244523161731856403098711121722383113
                        62229893423380308135336276614282806444486645238749
                        30358907296290491560440772390713810515859307960866
                        70172427121883998797908792274921901699720888093776
                        65727333001053367881220235421809751254540594752243
                        52584907711670556013604839586446706324415722155397
                        53697817977846174064955149290862569321978468622482
                        83972241375657056057490261407972968652414535100474
                        82166370484403199890008895243450658541227588666881
                        16427171479924442928230863465674813919123162824586
                        17866458359124566529476545682848912883142607690042
                        24219022671055626321111109370544217506941658960408
                        07198403850962455444362981230987879927244284909188
                        84580156166097919133875499200524063689912560717606
                        05886116467109405077541002256983155200055935729725
                        71636269561882670428252483600823257530420752963450");
        Console.WriteLine("Find the thirteen adjacent digits in the 1000-digit number that have the greatest product.  What is the value of this product?");
    }

    public override void ShowSolution()
    {
        const string fullNumber = "73167176531330624919225119674426574742355349194934"+
                        "96983520312774506326239578318016984801869478851843"+
                        "85861560789112949495459501737958331952853208805511"+
                        "12540698747158523863050715693290963295227443043557"+
                        "66896648950445244523161731856403098711121722383113"+
                        "62229893423380308135336276614282806444486645238749"+
                        "30358907296290491560440772390713810515859307960866"+
                        "70172427121883998797908792274921901699720888093776"+
                        "65727333001053367881220235421809751254540594752243"+
                        "52584907711670556013604839586446706324415722155397"+
                        "53697817977846174064955149290862569321978468622482"+
                        "83972241375657056057490261407972968652414535100474"+
                        "82166370484403199890008895243450658541227588666881"+
                        "16427171479924442928230863465674813919123162824586"+
                        "17866458359124566529476545682848912883142607690042"+
                        "24219022671055626321111109370544217506941658960408"+
                        "07198403850962455444362981230987879927244284909188"+
                        "84580156166097919133875499200524063689912560717606"+
                        "05886116467109405077541002256983155200055935729725"+
                        "71636269561882670428252483600823257530420752963450"; 
        int currInd = 0;
        int maxInd = 0;
        long maxProd = 0;
        while (currInd + 12 < fullNumber.Length) {
            long currProd = 1;
            for (int i = 0; i < 13; i++) {
                currProd *= (int)char.GetNumericValue(fullNumber[currInd+i]);
            }
            if (currProd > maxProd) {
                maxProd = currProd;
                maxInd = currInd;
            }
            currInd += 1;
        }
        Console.WriteLine($"The maximum product is {maxProd}, which is produced by the substring {fullNumber.Substring(maxInd,13)}");
    }
}

public class Problem9 : Problem
{
    public override void ShowProblemStatement()
    {
        Console.WriteLine("A Pythagorean triplet is a set of three natural numbers, a<b<c, for which a^2 + b^2 = c^2.");
        Console.WriteLine("For example, 3^2 + 4^2 = 9 + 16 = 25 = 5^2.");
        Console.WriteLine("There exists exactly one Pythagorean triplet for which a+b+c = 1000.  Find the product abc.");
    }

    public override void ShowSolution()
    {
        int a;
        int b;
        int c;
        for (a = 1; a < 998; a++) {
            for (b = a; b < 998; b++) {
                c = 1000 - a - b; 
                if (a*a + b*b == c*c) {
                    Console.WriteLine($"a={a}, b={b}, c={c}, so the product is {a*b*c}.");
                    return;
                }
            }
        }
    }
}

public class Problem10 : Problem
{
    public override void ShowProblemStatement()
    {
        Console.WriteLine("The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.");
        Console.WriteLine("Find the sum of all prime below two million.");
    }

    public override void ShowSolution()
    {
        // We will use a very similar method to the one used for Problem 7.  
        long result = 0;
        // FYI: VERY important that the above is long, else we overflow :) 
        List<int> primes = new();
        int n = 2;
        bool isPrime;
        while (n <= 2000000) {
            isPrime = true;
            foreach (int prime in primes) {
                // The following if statement was added, since we are going to be going through a LOT of primes / numbers.
                // We know that if we haven't found a prime factor by the time we get to sqrt(n), there are none, so this just checks for that case
                // (This approach avoids using Math.sqrt, which I assume is more costly than a simple multiplication)
                if (prime * prime > n) {
                    break;
                }
                if (n % prime == 0) {
                    isPrime = false;
                    break;
                }
            }
            if (isPrime) {
                primes.Add(n);
                result += n;
            }
            n += 1;
        }
        Console.WriteLine(result);
    }
}

public class Problem11 : Problem
{
    // This is just copied and pasted from the problem, with some +'s to make it one big string 
    string rawString = "08 02 22 97 38 15 00 40 00 75 04 05 07 78 52 12 50 77 91 08 " +
                        "49 49 99 40 17 81 18 57 60 87 17 40 98 43 69 48 04 56 62 00 " +
                        "81 49 31 73 55 79 14 29 93 71 40 67 53 88 30 03 49 13 36 65 " +
                        "52 70 95 23 04 60 11 42 69 24 68 56 01 32 56 71 37 02 36 91 " +
                        "22 31 16 71 51 67 63 89 41 92 36 54 22 40 40 28 66 33 13 80 " +
                        "24 47 32 60 99 03 45 02 44 75 33 53 78 36 84 20 35 17 12 50 " +
                        "32 98 81 28 64 23 67 10 26 38 40 67 59 54 70 66 18 38 64 70 " +
                        "67 26 20 68 02 62 12 20 95 63 94 39 63 08 40 91 66 49 94 21 " +
                        "24 55 58 05 66 73 99 26 97 17 78 78 96 83 14 88 34 89 63 72 " +
                        "21 36 23 09 75 00 76 44 20 45 35 14 00 61 33 97 34 31 33 95 " +
                        "78 17 53 28 22 75 31 67 15 94 03 80 04 62 16 14 09 53 56 92 " +
                        "16 39 05 42 96 35 31 47 55 58 88 24 00 17 54 24 36 29 85 57 " +
                        "86 56 00 48 35 71 89 07 05 44 44 37 44 60 21 58 51 54 17 58 " +
                        "19 80 81 68 05 94 47 69 28 73 92 13 86 52 17 77 04 89 55 40 " +
                        "04 52 08 83 97 35 99 16 07 97 57 32 16 26 26 79 33 27 98 66 " +
                        "88 36 68 87 57 62 20 72 03 46 33 67 46 55 12 32 63 93 53 69 " +
                        "04 42 16 73 38 25 39 11 24 94 72 18 08 46 29 32 40 62 76 36 " +
                        "20 69 36 41 72 30 23 88 34 62 99 69 82 67 59 85 74 04 36 16 " +
                        "20 73 35 29 78 31 90 01 74 31 49 71 48 86 81 16 23 57 05 54 " +
                        "01 70 54 71 83 51 54 69 16 92 33 48 61 43 52 01 89 19 67 48";
    // This guy just exists to print the problem to the console more easily. 
    string[,] stringGrid = new string[20,20];
    // This is the grid we'll actually work with. 
    int[,] grid = new int[20,20];
    public void SetupGrid() {
        // This function exists because I don't want to type 400 numbers into an array manually :) 
        string[] numStrings = rawString.Split(" ");
        int i = 0;
        int row = 0;
        int col = 0;
        foreach (string numString in numStrings) {
            row = i / 20;
            col = i % 20;
            stringGrid[row,col] = numString;
            grid[row,col] = Convert.ToInt32(numString);
            i++;
        }
    }
    
    public override void ShowProblemStatement()
    {
        SetupGrid();
        Console.WriteLine("In the 20x20 grid below, four numbers along a diagonal line have been marked in red.");
        for (int row = 0; row < 20; row++) {
            for (int col = 0; col < 20; col++) {
                if ((row == 6 && col == 8) || (row == 7 && col == 9) || (row == 8 && col == 10) || (row == 9 && col == 11)) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(stringGrid[row,col]);
                    Console.Write(" ");
                    Console.ResetColor();
                }
                else {
                    Console.Write(stringGrid[row,col]);
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("The product of these numbers is 26*63*78*14 = 1788696.");
        Console.WriteLine("What is the greatest product of four adjacent numbers in the same direction (up, down, left, right, or diagonally) in the 20x20 grid?");
    }

    public override void ShowSolution()
    {
        // We handle this in four cases separately below, to keep from having to make one god-awful ugly function. 
        // Each of the functions referenced below return a long array of the form [maximum product, startRow, startCol], 
        // where startRow and startCol are where we'd start in the grid to obtain that product (the direction we go from there, of course, varies depending which function we're looking at)
        long[] rowInfo = GetMaxRowProduct();
        long[] colInfo = GetMaxColProduct();
        long[] diagDownInfo = GetMaxDiagDownProduct();
        long[] diagUpInfo = GetMaxDiagUpProduct();
        // If all we cared about was showing the numerical answer, we could get away with the below.  
        long rowResult = rowInfo[0];
        long colResult = colInfo[0];
        long diagDownResult = diagDownInfo[0];
        long diagUpResult = diagUpInfo[0];
        long trueResult = Math.Max(rowResult,Math.Max(colResult,Math.Max(diagDownResult,diagUpResult)));
        Console.WriteLine(trueResult);
        // BUT I want to show what the numbers are and where they appear in the grid, so we do the below 
        // First, we figure out what method (row, col, diagDown, diagUp) produced the max result, and figure out where the numbers are in the grid based on that info 
        long[] rows = new long[4];
        long[] cols = new long[4];
        if (rowResult == trueResult) {
            for (int i = 0; i < 4; i++) {
                rows[i] = rowInfo[1];
                cols[i] = rowInfo[2] + i;
            }
        }
        else if (colResult == trueResult) {
            for (int i = 0; i < 4; i++) {
                rows[i] = colInfo[1] + i;
                cols[i] = colInfo[2];
            }
        }
        else if (diagDownResult == trueResult) {
            for (int i = 0; i < 4; i++) {
                rows[i] = diagDownInfo[1] + i;
                cols[i] = diagDownInfo[2] + i;
            }
        }
        else if (diagUpResult == trueResult) {
            for (int i = 0; i < 4; i++) {
                rows[i] = diagUpInfo[1] - i;
                cols[i] = diagUpInfo[2] + i;
            }
        }
        // Now that we have that info, we just print out the grid like we did in the problem statement, highlighting the elements that resulted in the max product
        for (int row = 0; row < 20; row++) {
            for (int col = 0; col < 20; col++) {
                if ((row == rows[0] && col == cols[0]) || (row == rows[1] && col == cols[1]) || (row == rows[2] && col == cols[2]) || (row == rows[3] && col == cols[3])) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(stringGrid[row,col]);
                    Console.Write(" ");
                    Console.ResetColor();
                }
                else {
                    Console.Write(stringGrid[row,col]);
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }
    }

    public long[] GetMaxRowProduct() {
        long maxProd = 0;
        long maxRow = 0;
        long maxCol = 0;

        long currProd;
        for (long row = 0; row < grid.GetLength(0); row++) {
            for (long col = 0; col + 3 < grid.GetLength(1); col++) {
                currProd = grid[row,col] * grid[row,col+1] * grid[row,col+2] * grid[row,col+3];
                if (currProd > maxProd) {
                    maxProd = currProd;
                    maxRow = row;
                    maxCol = col;
                }
            }
        }
        long[] result = new long[3];
        result[0] = maxProd;
        result[1] = maxRow;
        result[2] = maxCol;
        return result;
    }

    public long[] GetMaxColProduct() {
        long maxProd = 0;
        long maxRow = 0;
        long maxCol = 0;

        long currProd;
        for (long row = 0; row + 3 < grid.GetLength(0); row++) {
            for (long col = 0; col < grid.GetLength(1); col++) {
                currProd = grid[row,col] * grid[row + 1,col] * grid[row + 2,col] * grid[row + 3,col];
                if (currProd > maxProd) {
                    maxProd = currProd;
                    maxRow = row;
                    maxCol = col;
                }
            }
        }
        long[] result = new long[3];
        result[0] = maxProd;
        result[1] = maxRow;
        result[2] = maxCol;
        return result;
    }

    public long[] GetMaxDiagDownProduct() {
        long maxProd = 0;
        long maxRow = 0;
        long maxCol = 0;

        long currProd;
        for (long row = 0; row + 3 < grid.GetLength(0); row++) {
            for (long col = 0; col + 3 < grid.GetLength(1); col++) {
                currProd = grid[row,col] * grid[row + 1,col + 1] * grid[row + 2,col + 2] * grid[row + 3,col + 3];
                if (currProd > maxProd) {
                    maxProd = currProd;
                    maxRow = row;
                    maxCol = col;
                }
            }
        }
        long[] result = new long[3];
        result[0] = maxProd;
        result[1] = maxRow;
        result[2] = maxCol;
        return result;
    }

    public long[] GetMaxDiagUpProduct() {
        long maxProd = 0;
        long maxRow = 0;
        long maxCol = 0;

        long currProd;
        for (long row = 3; row < grid.GetLength(0); row++) {
            for (long col = 0; col + 3 < grid.GetLength(1); col++) {
                currProd = grid[row,col] * grid[row - 1,col + 1] * grid[row - 2,col + 2] * grid[row - 3,col + 3];
                if (currProd > maxProd) {
                    maxProd = currProd;
                    maxRow = row;
                    maxCol = col;
                }
            }
        }
        long[] result = new long[3];
        result[0] = maxProd;
        result[1] = maxRow;
        result[2] = maxCol;
        return result;
    }
}

public class Problem12 : Problem
{
    public override void ShowProblemStatement()
    {
        Console.WriteLine("The nth triangle number is generated by adding the first n consecutive natural numbers.  So the 7th triangle number would be 1 + 2 + 3 + 4 + 5 + 6 + 7 = 28.");
        Console.WriteLine("The first ten terms would be: 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...");
        Console.WriteLine("If we list the factors of the first seven triangle numbers, we find that 28 is the first triangle number with more than 5 divisors (1, 2, 4, 7, 14, 28).");
        Console.WriteLine("What is the value of the first triangle number to have over five hundred divisors?");
    }

    public override void ShowSolution()
    {
        // One secret to making this easier is to use our math knowledge, which is a little hard to explain briefly.  Long story short, we can skip finding every single factor 
        // of a given number by just finding the prime factorization and determining how many factors (prime or composite) there are based on that info. It turns out the amount of 
        // total factors can be found by taking each of the exponents in the prime factorization, adding 1, then multiplying all of that together!  This logic is contained in its own
        // function, NumberOfFactors.
        // For the purposes of this program, we represent the prime factorization as two lists which will always have the same length.  The first (primeFactors) contains the UNIQUE
        // prime factors of the number, and the other (primeExponents) contains the exponents corresponding to each of those factors.  That's the tricky part, the rest is just
        // setting it up to only check triangle numbers and stop when we get to one with > 500 divisors.  
        long step = 1;
        long triangleNumber = 1;
        while (NumberOfFactors(triangleNumber) <= 500) {
            step += 1;
            triangleNumber += step;
        }
        Console.WriteLine(triangleNumber);
    }

    public long NumberOfFactors(long N) {
        List<long> primeFactors = new();
        List<long> primeExponents = new();
        int numPrimes = 0;

        long currNum = N;
        long newPrimeFactor;
        while (currNum != 1) {
            // Extract the smallest (non-1) factor from the number (which is always prime, or there'd be a smaller one!), and update primeFactors and primeExponents accordingly
            newPrimeFactor = GetSmallestFactor(currNum);
            if (newPrimeFactor == primeFactors.LastOrDefault()) {
                primeExponents[numPrimes-1] += 1;
            } 
            else 
            {
                primeFactors.Add(newPrimeFactor);
                primeExponents.Add(1);
                numPrimes += 1;
            }
            currNum /= newPrimeFactor;
        }
        long result = 1;
        foreach (long exponent in primeExponents) {
            result *= exponent+1;
        }
        return result;
    }

    public long GetSmallestFactor(long N) {
        long potFactor = 2;
        while (potFactor*potFactor <= N) {
            if (N % potFactor == 0) {
                return potFactor;
            }
            potFactor += 1; 
        }
        return N;
    }
}

public class Problem13 : Problem
{
    string rawData = @"37107287533902102798797998220837590246510135740250
46376937677490009712648124896970078050417018260538
74324986199524741059474233309513058123726617309629
91942213363574161572522430563301811072406154908250
23067588207539346171171980310421047513778063246676
89261670696623633820136378418383684178734361726757
28112879812849979408065481931592621691275889832738
44274228917432520321923589422876796487670272189318
47451445736001306439091167216856844588711603153276
70386486105843025439939619828917593665686757934951
62176457141856560629502157223196586755079324193331
64906352462741904929101432445813822663347944758178
92575867718337217661963751590579239728245598838407
58203565325359399008402633568948830189458628227828
80181199384826282014278194139940567587151170094390
35398664372827112653829987240784473053190104293586
86515506006295864861532075273371959191420517255829
71693888707715466499115593487603532921714970056938
54370070576826684624621495650076471787294438377604
53282654108756828443191190634694037855217779295145
36123272525000296071075082563815656710885258350721
45876576172410976447339110607218265236877223636045
17423706905851860660448207621209813287860733969412
81142660418086830619328460811191061556940512689692
51934325451728388641918047049293215058642563049483
62467221648435076201727918039944693004732956340691
15732444386908125794514089057706229429197107928209
55037687525678773091862540744969844508330393682126
18336384825330154686196124348767681297534375946515
80386287592878490201521685554828717201219257766954
78182833757993103614740356856449095527097864797581
16726320100436897842553539920931837441497806860984
48403098129077791799088218795327364475675590848030
87086987551392711854517078544161852424320693150332
59959406895756536782107074926966537676326235447210
69793950679652694742597709739166693763042633987085
41052684708299085211399427365734116182760315001271
65378607361501080857009149939512557028198746004375
35829035317434717326932123578154982629742552737307
94953759765105305946966067683156574377167401875275
88902802571733229619176668713819931811048770190271
25267680276078003013678680992525463401061632866526
36270218540497705585629946580636237993140746255962
24074486908231174977792365466257246923322810917141
91430288197103288597806669760892938638285025333403
34413065578016127815921815005561868836468420090470
23053081172816430487623791969842487255036638784583
11487696932154902810424020138335124462181441773470
63783299490636259666498587618221225225512486764533
67720186971698544312419572409913959008952310058822
95548255300263520781532296796249481641953868218774
76085327132285723110424803456124867697064507995236
37774242535411291684276865538926205024910326572967
23701913275725675285653248258265463092207058596522
29798860272258331913126375147341994889534765745501
18495701454879288984856827726077713721403798879715
38298203783031473527721580348144513491373226651381
34829543829199918180278916522431027392251122869539
40957953066405232632538044100059654939159879593635
29746152185502371307642255121183693803580388584903
41698116222072977186158236678424689157993532961922
62467957194401269043877107275048102390895523597457
23189706772547915061505504953922979530901129967519
86188088225875314529584099251203829009407770775672
11306739708304724483816533873502340845647058077308
82959174767140363198008187129011875491310547126581
97623331044818386269515456334926366572897563400500
42846280183517070527831839425882145521227251250327
55121603546981200581762165212827652751691296897789
32238195734329339946437501907836945765883352399886
75506164965184775180738168837861091527357929701337
62177842752192623401942399639168044983993173312731
32924185707147349566916674687634660915035914677504
99518671430235219628894890102423325116913619626622
73267460800591547471830798392868535206946944540724
76841822524674417161514036427982273348055556214818
97142617910342598647204516893989422179826088076852
87783646182799346313767754307809363333018982642090
10848802521674670883215120185883543223812876952786
71329612474782464538636993009049310363619763878039
62184073572399794223406235393808339651327408011116
66627891981488087797941876876144230030984490851411
60661826293682836764744779239180335110989069790714
85786944089552990653640447425576083659976645795096
66024396409905389607120198219976047599490197230297
64913982680032973156037120041377903785566085089252
16730939319872750275468906903707539413042652315011
94809377245048795150954100921645863754710598436791
78639167021187492431995700641917969777599028300699
15368713711936614952811305876380278410754449733078
40789923115535562561142322423255033685442488917353
44889911501440648020369068063960672322193204149535
41503128880339536053299340368006977710650566631954
81234880673210146739058568557934581403627822703280
82616570773948327592232845941706525094512325230608
22918802058777319719839450180888072429661980811197
77158542502016545090413245809786882778948721859617
72107838435069186155435662884062257473692284509516
20849603980134001723930671666823555245252804609722
53503534226472524250874054075591789781264330331690";
    public override void ShowProblemStatement()
    {
        Console.WriteLine("Work out the first ten digits of the sum of the following one-hundred 50-digit numbers.");
        Console.WriteLine(rawData);
    }

    public override void ShowSolution()
    {
        // We stored all those numbers as a big ugly string.  First we split the string into the individual numbers, then we add up the relevant parts of the numbers 
        // Since we only care about the first 10 digits of the sum, we don't need all of the digits of each number (which is lucky because they're too big to store in even a long...)
        // We only need the first 12 digits of each of the numbers (2 more than the number of digits we care about in the result, because the amount of numbers we're adding is 100 = 10^2)
        string[] arrayOfStrings = rawData.Split("\n");
        long sum = 0;
        for (int i = 0; i < 100; i++) {
            sum += Convert.ToInt64(arrayOfStrings[i][..12]);
        }
        Console.WriteLine(sum.ToString()[..10]);
    }
}

public class Problem14 : Problem
{
    public override void ShowProblemStatement()
    {
        Console.WriteLine("The following iterative sequence is defined for the set of positive integers: given a term in the sequence n...");
        Console.WriteLine("- If n is even, the next term is n/2");
        Console.WriteLine("- If n is odd, the next term is 3*n+1");
        Console.WriteLine("For example, starting with 13, we would generate the following sequence:");
        Console.WriteLine("13 -> 40 -> 20 -> 10 -> 5 -> 16 -> 8 -> 4 -> 2 -> 1 -> 4 -> 2 -> 1 -> ...");
        Console.WriteLine("This particular sequence contains 10 non-repeating terms (starting at 13 and finishing at the first instance of 1). In fact, every choice of starting number");
        Console.WriteLine("that has been checked so far ends in the same pattern (4 -> 2 -> 1 -> 4 -> 2 -> 1 -> ...). However, it is not known whether this is true for ALL natural numbers.");
        Console.WriteLine("(This is not from lack of trying; mathematicians have checked ridiculously large numbers trying to find a counter-example).");
        Console.WriteLine("Our question for this problem: Which starting number, under one million, produces the longest chain of non-repeating numbers (hint: all such chains will end exactly when they reach 1)");
        Console.WriteLine("NOTE: only the starting number needs to be under one million; from there it is possible that the terms go above one million.");
    }

    public override void ShowSolution()
    {
        // After running this and it taking longer than some of the earlier problems, it occurred to me that we can cross off many of these numbers without checking the full
        // Collatz sequence starting with that number, if they appear in another starting number's Collatz sequence.  However, accounting for this fact would require a pretty significant
        // rewrite (for example, we could start with an array of all the numbers from 1 to 1 million, and cross off numbers as they appear in ANY collatz sequence.
        // Our program still runs pretty fast (around 2 seconds), so I will hold off for now
        int longestSeqLength = 0;
        int maximizer = 0;
        for (int i = 1; i < 1000000; i++) {
            int newLength = CollatzLength(i);
            if (newLength > longestSeqLength) {
                longestSeqLength = newLength;
                maximizer = i;
            }
        }
        Console.WriteLine($"The longest Collatz sequence with a starting term under 1000000 is produced by the starting term {maximizer}, and has {longestSeqLength} terms");
    }

    public int CollatzLength(int N) {
        // Worth mentioning: this function only guarantess to terminate under the assumption that the Collatz conjecture is true, which is unknown.  However, I will assume that
        // I will not be the lucky person to find a counterexample while trying to solve a Project Euler problem that thousands of other people have already solved 
        long currTerm = N;
        int seqLength = 1;
        while (currTerm != 1) {
            if (currTerm % 2 == 0) {
                currTerm = currTerm / 2;
            }
            else {
                currTerm = 3 * currTerm + 1;
            }
            seqLength += 1;
        }
        return seqLength;
    }
}

public class Problem15 : Problem
{
    public override void ShowProblemStatement()
    {
        Console.WriteLine("Starting in the top left corner of a 2x2 grid, and only being able to move to the right and down, there are exactly 6 routes to the bottom right corner.");
        Console.WriteLine("How many such routes are there through a 20x20 grid?");
    }

    public override void ShowSolution()
    {
        // Mathematically, this is easy enough.  Any path will contain exactly 40 total movements: 20 right movements, and 20 down movements, it's just a matter of the order of those movements.  
        // A little combinatoric magic tells us that we are really just choosing 20 out of the 40 move-slots to place the right movements, and the rest will be down movements (or vice versa).
        // So the answer is 40C20, or 40!/[(20!)*(20!)].  BUT, computationally this is not so easy, because 40! has 48 digits, too big to fit into even a long.  
        // Of course, 20! is also pretty dang big, especially when squared, which will tame the true result down to a reasonable number in the final calculation.  
        // The tricky part is keeping the numbers reasonable along the way...
        decimal N = 20;
        decimal result = 1;
        for (decimal i = 1; i <= N; i++) {
            result *= (N+i)/i;
        }
        Console.WriteLine(result.ToString("0."));
    }
}