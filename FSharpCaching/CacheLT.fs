namespace FSharpCaching 

open System.Collections.Concurrent


// (credits go to Lawrence)
module CacheLT = 

    type private StoreAcess<'key, 'value> = { get: 'key -> 'value option; set: 'key -> 'value -> unit }

    let private getConcurrentDictAccess<'key, 'value>() = 
        let store = new ConcurrentDictionary<'key, 'value>()

        let storeSet = fun key value -> 
            store.TryAdd(key, value) |> ignore 

        let storeGet = fun key ->
            match store.ContainsKey(key) with 
                | true -> Some(store.[key])  
                | _ -> None 

        { get = storeGet; set = storeSet}



    
    let cache<'key, 'value> (get: 'key -> 'value option) (set: 'key -> 'value -> unit) (generator: 'key -> 'value) (key: 'key) = 
        match get(key) with 
            | Some(x) -> x 
            | None -> 
                let value = generator(key) 
                set key value
                value         


    let getOrAdd<'key, 'value> generator = 
        let repoAccess = getConcurrentDictAccess<'key, 'value>()
        cache repoAccess.get repoAccess.set generator 

         


         


