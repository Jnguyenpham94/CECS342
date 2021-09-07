(* text-based game in which the player attempts to shoot cannonballs at a target that is some
distance away. The player repeatedly enters an angle to fire the cannon at and an amount of gunpowder
to use. The game calculates how far the cannonball will fly under those parameters and continues until the
cannonball falls within 1 meter of the target. 
*)
open System

// returns float value 0 - 1000 m inclusive.
//multiply rand to get it within 0 - 1000 m
let placeTarget () = 
    let rand = (new Random()).NextDouble() * 1000.0
    rand

//TODO: getAngle needs some work
//prompts user to enter angle of fire
let getAngle () =
    printfn "Enter angle between 0 - 90"
    let mutable amount = Console.ReadLine() |> int
    while amount < 0 && amount > 90 do
        printfn "Angle is out of bounds. Enter angle between 0 - 90"
        amount <- Console.ReadLine() |> int
    float amount

//TODO: getGunpowder I AM HERE!!!!
//prompts user for amount of gunpowder in kg to fire cannon
let getGunpowder () =
    printfn "Enter positive float: "
    let amount = Console.ReadLine() |> float
    abs amount //abs the amount in case someone inputs negative

//TODO: calculateDistance needs some work
//angle of amount of gunpowder
//returns horizontal distance of projectile
//no prints
let calculateDistance angle gunpowder =
    let initVel = 30
    initVel

//TODO: isHit needs some work
//location of target and distance projectile has moved
//hit occurs if projectile lands within 1.0 m of target
//no let and prints allowed
let isHit location distance =
    if location - distance <= 1 && location - distance >= 0 then
        true
    else
        false

//main
[<EntryPoint>]
let main argv =
    printfn "main stuff here"
    let target = placeTarget()
    printfn "distance to target: %f" target
    let mutable angle = getAngle ()
    let mutable powder = getGunpowder ()
    let mutable travel = calculateDistance angle powder
    let mutable hit = isHit target travel
    while hit = false do
        angle <- getAngle ()
        powder <- getGunpowder ()
        travel <- calculateDistance angle powder
    0