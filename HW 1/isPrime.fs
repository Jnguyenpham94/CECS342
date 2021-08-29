
open System
//program to check if inputted int is prime

printfn "Input a value to check if prime: "
let x = Console.ReadLine() |> int
//boolean to stop looping when divisor is found
let found = false

if x % 2 = 0 then
    printfn "Even so NOT PRIME"
else
    let mutable i = 0
    while i < x && found = false do
        i <- i + 1 // <- is for reassignment / mutation
