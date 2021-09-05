(* text-based game in which the player attempts to shoot cannonballs at a target that is some
distance away. The player repeatedly enters an angle to fire the cannon at and an amount of gunpowder
to use. The game calculates how far the cannonball will fly under those parameters and continues until the
cannonball falls within 1 meter of the target. 
*)
open System

// returns float value 0 - 1000 m inclusive.
let placeTarget () = 
    let rand = (new Random()).NextDouble()
    rand

//prompts user to enter angle of fire
let getAngle  () =
    printfn "Enter angle between 0 - 90"
    let mutable amount = Console.ReadLine |> int
    while amount < 0 && amount > 90 do
        printfn "Angle is out of bounds. Enter angle between 0 - 90"
        amount <- Console.ReadLine |> int
    float amount

//prompts user for an angle to fire cannon
let getGunpowder () =
    ()

//angle of amount of gunpowder
//returns horizontal distance of projectile
//no prints
let calculateDistance angle gunpowder =
    ()

//location of target and distance projectile has moved
//hit occurs if projectile lands within 1.0 m of target
//no let and prints allowed
let isHit location distance =
    ()

//main
[<EntryPoint>]
let main argv =
    printfn "main stuff here"

    0