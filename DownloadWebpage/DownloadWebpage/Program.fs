open System.Net
open System
open System.IO

let fetchUrl callback url =
    let req = WebRequest.Create(Uri(url))
    use resp = req.GetResponse()
    use stream = resp.GetResponseStream()
    use reader = new IO.StreamReader(stream)
    callback reader url


let myCallback (reader:IO.StreamReader) url = 
    let html = reader.ReadToEnd()
    let html1000 = html.Substring(0,10000)
    printfn "Downloaded %s. First 10000 is %s" url html1000
    html

 
let fetchFunction = fetchUrl myCallback //thus the callback parameter and fetchUrl parameters are "baked in" meaning they do not have to be called specifically

let google = fetchFunction "https://google.com" //note we are just passing the URL parameter now
let bbc = fetchFunction "https://www.bbc.co.uk"


let sites = ["https://www.vox.com/"; "https://uk.reuters.com/"; "https://www.pcgamer.com/"]  //creates a list

let result = sites |> List.map fetchFunction //passes each item of the list into fetchFunction

Console.ReadLine() |> ignore