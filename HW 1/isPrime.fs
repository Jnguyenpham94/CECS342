
open System
//program to check if inputted int is prime

printfn "Input a value to check if prime: "
let x = Console.ReadLine() |> int
//boolean to stop looping when divisor is found or just to stop while loop for special cases
let mutable stop = false
let mutable found = false //if prime F otherwise stays T

if x % 2 = 0 then
    printfn "%d is even so NOT PRIME" x
elif x <= 1 then
    printfn "%d is NOT PRIME" x
else
    let mutable i = 3 //all even numbers are prime so start at first odd int after
    while float i <= sqrt (float x) && stop <> true do
        //check for largest int that can be divisor of x
        //if greater STOP
        if x % i = 0 then
            found <- true
            stop <- true
        else
            i <- i + 2 // iterate 2 values to skip even values
    
    if found = true then
        printfn "%d is NOT PRIME" x
    else
        printfn "%d is PRIME" x