
open System
//program to check if inputted int is prime

printfn "Input a value to check if prime: "
let x = Console.ReadLine() |> int
//boolean to stop looping when divisor is found or just to stop while loop for special cases
let mutable stop = false

if x % 2 = 0 then
    printfn "Even so NOT PRIME"
elif x = 2 then
    printfn "Is 2 which is PRIME"
else
    let mutable i = 1 //all even numbers are not prime so start at first non-prime int
    while i < x || stop <> false do
        if i > sqrt x then
            stop = true
        if x % i = 0 then
            stop = true
        i <- i + 2 // <- is for reassignment / mutation
