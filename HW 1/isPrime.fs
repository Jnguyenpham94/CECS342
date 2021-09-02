
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
    while i < x && stop <> true do
        if float i > sqrt (float x) then //check for largest int that can be divisor or x
            stop <- true
            printfn "%d NOT PRIME" x
        elif x % i = 0 then
            stop <- true//TODO: need to get this output cases done
            printfn "%d is NOT PRIME" x
        else
            stop <- true
            printfn "%d is PRIME" x
        i <- i + 2 // iterate 2 values to skip even values