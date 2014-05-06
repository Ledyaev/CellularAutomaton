class Life {

    table: Array<Array<Cell>>;
    city: Array<Array<boolean>>;
    newCity: Array<Array<boolean>>;
    areaSize: number;
    Initialize(n:number)
    {
        this.areaSize = n;
        var area = document.getElementsByClassName("playAreaContainer")[0];
        var playAreaTable = document.createElement('table');
        playAreaTable.width = String(n * 10);
        playAreaTable.className = "playArea";
        playAreaTable.onclick = (e: MouseEvent) => { this.ChangeState(e) };
        this.table = new Array<Array<Cell>>(n);
        this.city = new Array<Array<boolean>>(n);
        this.newCity = new Array<Array<boolean>>(n);
        var date = new Date();
        var time = date.getTime()/1000;
        for (var i = 0; i < n; i++) {
            var row = <HTMLTableRowElement> playAreaTable.insertRow(-1);
            this.table[i] = new Array<Cell>(n);
            this.city[i] = new Array<boolean>(n);
            this.newCity[i] = new Array<boolean>(n);
            for (var j = 0; j < n; j++) {
                var cell = <Cell>row.insertCell(-1);                
                cell.className = "dead cell";
                cell.i = i;
                cell.j = j;
                this.table[i][j] = cell;
                this.city[i][j] = false;
                this.newCity[i][j] = false;
            }
        }
        time = new Date().getTime()/1000- time;
        window.alert(time);
        var button = document.getElementById("step");
        button.onclick = (e: MouseEvent) => { this.CalculateNewState() };
        area.appendChild(playAreaTable);
    }

    ChangeState(e: MouseEvent): any {
        var item = <Cell>e.target;
        if (item != null) {
            var i = item.i;
            var j = item.j;
            if (item.classList.contains("dead")) {
                item.className = "alive cell";
                this.table[i][j] = item;
                this.city[i][j] = true;
            }
            else {
                item.className = "dead cell";
                this.table[i][j] = item;
                this.city[i][j] = false;
            }
        }
    }

    CalculateNewState(): any {
        for (var i = 0; i < this.areaSize; i++)
            for (var j = 0; j < this.areaSize; j++) {
                var aliveNow = this.city[i][j];
                var numberNeighbors = this.CountOfNeighbors(i, j);
                if (aliveNow != (this.newCity[i][j] = (numberNeighbors == 2) && aliveNow || (numberNeighbors == 3)))
                    this.table[i][j].className = aliveNow ? "dead cell" : "alive cell";
            }
        for (var i = 0; i < this.areaSize; i++)
            for (var j = 0; j < this.areaSize; j++) {
                this.city[i][j] = this.newCity[i][j];
            }
    }

    CountOfNeighbors(x: number, y: number): number {
        return this.GetNeighborValue(x, y - 1) + this.GetNeighborValue(x, y + 1) +
            this.GetNeighborValue(x - 1, y - 1) + this.GetNeighborValue(x - 1, y) + this.GetNeighborValue(x - 1, y + 1) +
            this.GetNeighborValue(x + 1, y - 1) + this.GetNeighborValue(x + 1, y) + this.GetNeighborValue(x + 1, y + 1);
    }

    GetNeighborValue(x: number, y: number):number{
        if (x >= this.areaSize || x<0)
            x =Math.abs( Math.abs(x)-this.areaSize);
        if (y >= this.areaSize || y < 0)
            y =Math.abs(Math.abs(y)- this.areaSize);
        return this.city[x][y] ? 1 : 0;
    }
}

var life = new Life();
life.Initialize(1000);

