
open System
//program to check if inputted int is prime

printfn "Input a value to check if prime: "
let x = Console.ReadLine() |> int
//boolean to stop looping when divisor is found or just to stop while loop for special cases
let mutable found = false

if x % 2 = 0 || x = 1 then
    printfn "Even so NOT PRIME"
elif x = 2 then
    printfn "Is 2 which is PRIME"
else
    let mutable i = 3 //all even numbers are not prime so start at first non-prime int
    while i < x || found = false do
        if i = sqrt x then
            found = true
        i <- i + 2 // <- is for reassignment / mutation
