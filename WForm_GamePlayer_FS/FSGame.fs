namespace System.Windows.Forms.GamePlayerFS

module gameControl = 
    open System.Windows.Forms
    open System.Timers
    open System.Drawing
    open System
    open Draw

    type QuickGame<'a> = 
        {
        draw: ('a -> Image.Type) Option; 
        tick: ('a -> 'a) Option; 
        mouse: ('a -> 'a) Option
        key: ('a -> 'a) Option
        initial:'a
        }

    type GamePlayerControl(width, height, tps, game:QuickGame<'a>) as this = 
        inherit System.Windows.Forms.Form()
        let tick = new Timer()

        let choose (v:'b Option)(e:'b) = 
            match v with
            | Some v' -> v'
            | None -> e
            
        let mutable state = game.initial

        let draw() = 
            if this.InvokeRequired then
                let act = new Action(this.Refresh)
                this.Invoke(act) |> ignore
            else this.Refresh()
                
        do 
            this.Size <- new Size(width, height)
            tick.Elapsed.Add (fun _ -> (state <- (choose game.tick id) state) |> draw)
            this.Paint.Add (fun e -> Image.draw e.Graphics ((choose game.draw (fun _ -> Image.empty)) state))
            
            this.ClientSize <- new System.Drawing.Size(width, height)
            tick.AutoReset <- true
            this.Show()
            tick.Start()
            
        member this.TicksPerSecond 
            with get() = int (1.0 / tick.Interval / 1000.0)
            and set(value:int) = tick.Interval <- 1000.0 * (1.0 / float value)
            
        member this.CurrentGame = game