namespace FSharpCaching 

open CacheLT
open Cache
open Cache2

module Main = 


    let getOrAddAndPrint<'key, 'value> (getOrAddFunc : ('key -> 'value) -> ('key -> 'value)) generator (keys: 'key list) =

        let myGetOrAdd = getOrAddFunc generator

        let results = 
            keys
                |> List.map myGetOrAdd
        printfn "%A" results 
        printfn "%A" (myGetOrAdd keys.Head)


    [<EntryPoint>]
    let main argv = 
        printfn "%A" argv


        let generator i = 
            printfn "In generator for %A" i
            i + 10 

        getOrAddAndPrint getOrAdd generator [0..10]
        getOrAddAndPrint getOrAdd2 generator [0..10]


        getOrAddAndPrint get generator [0..10]




        0 // return an integer exit code
