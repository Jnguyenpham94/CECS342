(* text-based game in which the player attempts to shoot cannonballs at a target that is some
distance away. The player repeatedly enters an angle to fire the cannon at and an amount of gunpowder
to use. The game calculates how far the cannonball will fly under those parameters and continues until the
cannonball falls within 1 meter of the target. 
*)
open System

// returns float value 0 - 1000 m inclusive.
//multiply rand to get default 0.0 - 1.0 to 0.0 - 1000.0 m
let placeTarget () = 
    let rand = (new Random()).NextDouble() * 1000.0
    rand


//prompts user to enter angle of fire
let getAngle () =
    printfn "Enter angle between 0 - 90: "
    let mutable amount = Console.ReadLine() |> int
    while amount < 0 && amount > 90 do
        printfn "Angle is out of bounds. Enter angle between 0 - 90: "
        amount <- Console.ReadLine() |> int
    float amount


//prompts user for amount of gunpowder in kg to fire cannon
let getGunpowder () =
    printfn "Enter positive float for gunpowder: "
    let amount = Console.ReadLine() |> float
    abs amount //abs the amount in case someone inputs negative


//angle in degrees
//amount of gunpowder in kg 
//returns horizontal distance of projectile
//no prints allowed
//projectile distance formula on flat surface used: Sin(2theta)v**2/g
//1 deg * pi/180 = 0.01745 rad or 1 deg * pi/180 deg
let calculateDistance angle gunpowder =
    let v = 30.0 * gunpowder //initial velocity
    let theta = angle * (Math.PI/180.0)
    let g = 9.81 //gravity accel
    let distance = (v**2.0/g)*Math.Sin(2.0*theta)
    distance

//location of target and distance projectile has moved
//hit occurs if projectile lands within 1.0 m of target
//no let and prints allowed
let isHit location distance =
    if location - distance <= 1.0 && location - distance >= 0.0 then
        true
    else
        false

//main
[<EntryPoint>]
let main args =
    printfn "GAME START!"
    let mutable shots = 0 //keeps track of how many shots fired
    let target = placeTarget()
    printfn "distance to target: %f" target
    let mutable angle = getAngle ()
    let mutable powder = getGunpowder ()
    let mutable travel = calculateDistance angle powder
    let mutable hit = isHit target travel
    shots <- shots + 1
    if hit = true then
        printfn "WOW got a hit on the first try!!!"
    else
        while hit = false do
            angle <- getAngle ()
            powder <- getGunpowder ()
            travel <- calculateDistance angle powder
            hit <- true
            shots <- shots + 1
            if hit = true then
                printfn "You have hit the Target. YOU WIN!!!"
                printfn "It took %d shots" shots
            else
                printfn "You are %f off the target" 4.0//target-travel
                printfn "It took %d shots" shots
    0