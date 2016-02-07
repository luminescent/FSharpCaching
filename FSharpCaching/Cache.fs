namespace FSharpCaching 

open System.Collections.Concurrent

module Cache = 
    
    type ICacheStore<'key, 'value> = 
        abstract member Get: 'key -> 'value option 
        abstract member Set: 'key -> 'value -> unit


    let cache2<'key, 'value> (store: ICacheStore<'key, 'value>) (generator : 'key -> 'value) (key: 'key) = 
        match store.Get key with 
            | Some(x) ->  x
            | None -> 
                let value = generator key 
                store.Set key value
                value

    type ConcurrentDictonaryStore<'key, 'value>() = 
        let store = new ConcurrentDictionary<'key, 'value>()
        interface ICacheStore<'key, 'value> with
            member this.Get key = 
                match store.ContainsKey key with
                    | true -> Some(store.[key])
                    | _ -> None
            member this.Set (key: 'key) (value: 'value) =
                store.[key] <- value 


    let getOrAdd2<'key, 'value> generator =
        let store = new ConcurrentDictonaryStore<'key, 'value>() :> ICacheStore<'key, 'value>
        cache2 store generator 



