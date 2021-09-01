
open System
//program to check if inputted int is prime

printfn "Input a value to check if prime: "
let x = Console.ReadLine() |> int
//boolean to stop looping when divisor is found or just to stop while loop for special cases
let mutable stop = false

if x % 2 = 0 then
    printfn "%d is even so NOT PRIME" x
else
    let mutable i = 1 //all even numbers are prime so start at first non-prime int
    while i < x || stop <> false do
        if i > sqrt y then //check for largest int that can be divisor or x
            stop = true
            printfn "NOT PRIME"
        elif x % i = 0 then
            stop = true
            printfn "IS PRIME"
        else
            i <- i + 2 // <- is for reassignment / mutation iterate 2 values to skip even values