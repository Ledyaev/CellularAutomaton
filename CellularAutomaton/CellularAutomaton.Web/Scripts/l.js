var LifeGame = {

  // Constants
  checkSize: 8,

  // Generation
  curGen: {},
  stepNo: 0,
  genSigns: [],
  lastCode: '',
  
    //Rules
  birth: 3,
  overcrowding: 3,
  loneliness: 2,

  // Viewport coordinates
  offsetX: 0,
  offsetY: 0,

  // Drawing
  lastDraw: null,

  // Timer
  timerId: null,

  // Record steps
  recordSteps: '',
  recordOn: false,
  lastRecorded: false,

  // Field panning
  panningStart: null,

  /*
    Make step function
  */
  doStep: function()
  {
    var newGen = {}, testedChecks = {};
    for(var y0 in this.curGen) for(var x0 in this.curGen[y0])
    {
      x0 = parseInt(x0);
      y0 = parseInt(y0);
      for(var y = y0-1; y <= y0+1; ++ y) for(var x = x0-1; x <= x0+1; ++ x)
      {
        if(this.hasCheck(testedChecks, x, y))
          continue;
        var neighbourCount = 0;
        for(var yt = y-1; yt <= y+1; ++ yt) for(var xt = x-1; xt <= x+1; ++ xt)
        {
          if(!(yt == y && xt == x) && this.hasCheck(this.curGen, xt, yt))
          {
            ++ neighbourCount;
          }
        }
        this.setCheck(testedChecks, x, y);
        if(this.hasCheck(this.curGen, x, y))
        {
          if(neighbourCount >= this.loneliness && neighbourCount <=this.overcrowding)
          {
            this.setCheck(newGen, x, y);
          }
        }
        else
        {
          if(neighbourCount == this.birth)
          {
            this.setCheck(newGen, x, y);
          }
        }
      }
    }
    this.curGen = newGen;
    ++ this.stepNo;
  },

  /*
    Draw generation function
  */
  draw: function()
  {
    var cnv = $('#field')[0];
    var frame = $('#frame')[0];
    var dc = (cnv && cnv.getContext) ? cnv.getContext('2d') : null;
    if(!dc)
    {
      alert('No drawing context');
      return false;
    }

    // Calc dimensions
    var dims = this.getDimensions(this.curGen);

    var width = dims.maxX - dims.minX + 1, height = dims.maxY - dims.minY + 1;
    var pWidth = this.checkSize * width, pHeight = this.checkSize * height;
    var pLeft = this.checkSize * (dims.minX - this.offsetX - 0.5) + frame.clientWidth / 2;
    var pTop = this.checkSize * (dims.minY - this.offsetY - 0.5) + frame.clientHeight / 2;

    cnv.width = pWidth;
    cnv.height = pHeight;
    cnv.style.width = pWidth + 'px';
    cnv.style.height = pHeight + 'px';
    cnv.style.left = pLeft + 'px';
    cnv.style.top = pTop + 'px';

    dc.fillStyle = "rgb(255,255,255)";
    dc.fillRect (0, 0, pWidth, pHeight);

    dc.fillStyle = "rgb(0,0,0)";
    for(var y in this.curGen)
    {
      for(var x in this.curGen[y])
      {
        dc.fillRect (this.checkSize * (x - dims.minX), this.checkSize * (y - dims.minY), this.checkSize, this.checkSize);
      }
    }

    $('#info').html('Поколение:' + this.stepNo + '<br/>Живые клетки:' + dims.checkCount);

    if(!dims.checkCount)
      return false;
    else
      return true;
  },

  /*
    Get generation dimensions
  */
  getDimensions: function(field)
  {
    var checkCount = 0;
    var minX = 1e20, maxX = -1e20, minY = 1e20, maxY = -1e20;
    for(var y in field)
    {
      for(var x in field[y])
      {
        minY = Math.min(minY, y);
        maxY = Math.max(maxY, y);
        minX = Math.min(minX, x);
        maxX = Math.max(maxX, x);
        ++ checkCount;
      }
    }
    if(!checkCount)
      minX = maxX = minY = maxY = 0;

    return { minX: minX, maxX: maxX, minY: minY, maxY: maxY, checkCount: checkCount };
  },

  /*
    Show current controls state
  */
  updateControls: function()
  {
      $('#tbStart')[0].className = this.timerId ? 'disabled btn btn-sm btn-default' : 'btn btn-sm btn-default';
  },

  /*
    Start, stop and make step handlers
  */
  start: function()
  {
    if(!this.timerId)
    {
      this.timerId = setInterval(function(){ LifeGame.step(); }, 250);
      this.updateControls();
    }
  },

  stop: function()
  {
    if(this.timerId)
    {
      clearInterval(this.timerId);
      this.timerId = null;
      this.updateControls();
    }
  },

  step: function()
  {
    if(this.recordOn && !this.lastRecorded)
      this.recordStep(this.lastCode != '' ? this.lastCode : this.stepCode());

    var time = new Date().getTime();
    this.doStep();
    time = new Date().getTime()- time;
    if (time > 1000) {
        this.stop();
        alert('Расчет был остановлен, так как дальнейшее выполнение повлечет за собой замедление работы браузера.');
    }
    var curCode = this.stepCode();
    if(this.recordOn)
    {
      this.recordStep(curCode);
    }
    if(!this.draw())
    {
      this.recordOn = false;
      this.stop();
      alert('Колония польностью вымерла на ' + this.stepNo + ' ходу');
      return;
    }
    if(this.lastCode == curCode)
    {
      this.stop();
      alert('Статическая колония');
    }
    else for(var n = 0; n < this.genSigns.length; ++ n)
    {
      if(this.genSigns[n] == curCode)
      {
        this.stop();
        alert('Обнаружен цикл');
      }
    }
    this.genSigns.push(curCode);
    while(this.genSigns.length > 16)
      this.genSigns.shift();
    this.lastCode = curCode;
  },

  zoom: function(dir)
  {
    this.checkSize = (dir > 0) ? Math.min(16, Math.round(this.checkSize * 1.6))
      : Math.max(1, Math.round(this.checkSize / 1.6));
    this.draw();
  },

  center: function()
  {
    this.panningStart = null;
    var dims = this.getDimensions(this.curGen);
    this.offsetX = Math.round((dims.minX + dims.maxX) / 2);
    this.offsetY = Math.round((dims.minY + dims.maxY) / 2);
    this.draw();
  },

  reset: function(ask)
  {
    this.stop();
    if (ask && !confirm('Очистить поле и сбросить все параметры?'))
      return;
    this.panningStart = null;
    this.curGen = {};
    this.stepNo = 0;
    this.recordSteps = '';
    this.recordOn = false;
    this.offsetX = this.offsetY = 0;
    this.genSigns = [];
    this.lastSign = '';
    this.updateControls();
    this.draw();
  },

  applyRules: function (birth, overcrowding, loneliness) {
      if (birth != "" && overcrowding != "" && loneliness != "") {
          this.stop();
          if (!confirm('Применить правило?'))
              return;
          this.panningStart = null;
          this.curGen = {};
          this.stepNo = 0;
          this.recordSteps = '';
          this.recordOn = false;
          this.offsetX = this.offsetY = 0;
          this.genSigns = [];
          this.lastSign = '';
          this.birth = birth,
          this.overcrowding = overcrowding,
          this.loneliness = loneliness,
          this.updateControls();
          this.draw();
      }
  },
  
  save: function () {
      var area = JSON.stringify(this.curGen);
      var rules = this.birth + " " + this.overcrowding + " " + this.loneliness;
      var discription = $("#saveAsDiscription").val();
      var tags = $("#saveAsTags").val();
      var name = $("#saveAsName").val();
      $.ajax({
          type: "POST",
          url: "/Automaton/Save",
          data: "area=" + area+"&rules="+rules+"&discription="+discription+"&tags="+tags+"&name="+name,
      success: function (data) {
          //$("#dialogs ul").empty();
          if (data == false) {
              alert("У вас уже есть данная конфигурация.");
          }
          else {
              $('#saveModal').modal('hide');
              $("#saveAsDiscription").val("");
              $("#name").val("");
              $("#saveAsTags").val("");
              alert("Конфигурация была успешно сохранена.");
              window.location = "/Automaton/ShowAutomaton/"+data;
          }
      }
  });
  },
  
  resave: function () {
      var area = JSON.stringify(this.curGen);
      var rules = this.birth + " " + this.overcrowding + " " + this.loneliness;
      var discription = $("#saveAsDiscription").val();
      var tags = $("#saveAsTags").val();
      var name = $("#saveAsName").val();
      var id = $("#hdAutomatonId").val();
      $.ajax({
          type: "POST",
          url: "/Automaton/Resave",
          data: "area=" + area + "&rules=" + rules + "&discription=" + discription + "&tags=" + tags + "&name=" + name+"&id="+id,
          success: function (data) {
            //  $("#dialogs ul").empty();
              if (data == true) {
                  $('#saveModal').modal('hide');
                  $("#saveAsDiscription").val("");
                  $("#saveAsName").val("");
                  $("#saveAsTags").val("");
                  alert("Конфигурация была успешно пересохранена.");
                  location.reload();
              }
          }
      });
  },

  record: function()
  {
    this.recordOn = !this.recordOn;
    this.updateControls();
  },

  /*
    Frame mouse handlers
  */
  frameMouseDown: function(ev)
  {
    if(ev.ctrlKey)
    {
      // Start panning
      var x = ev.offsetX || ev.layerX;
      var y = ev.offsetY || ev.layerY;
      var field = $('#field')[0];
      this.panningStart = {
        x: x, y: y,
        left: parseInt(field.style.left), top: parseInt(field.style.top),
        offX: this.offsetX, offY: this.offsetY
      };
      this.lastDraw = null;
    }
    else
    {
      if(this.timerId)
        return;
      // Toggle check
      var cc = this.getCheckCoords(ev);
      this.toggleCheck(this.curGen, cc.x, cc.y);
      this.lastDraw = {
        x: cc.x,
        y: cc.y,
        s: this.hasCheck(this.curGen, cc.x, cc.y) ? '+' : '-'
      };
      this.draw();
    }
    // Disable drag-n-drop in FF3.5
    if(ev.preventDefault)
      ev.preventDefault();
  },

  frameMouseUp: function(ev)
  {
    if(this.panningStart)
    {
      var field = $('#field')[0];
      this.setOffsets();
      this.panningStart = null;
      this.draw();
    }
    this.lastDraw = null;
  },

  frameMouseMove: function(ev)
  {
    if(this.panningStart)
    {
      var x = ev.offsetX || ev.layerX;
      var y = ev.offsetY || ev.layerY;
      var field = $('#field')[0];
      field.style.left = (0 + x - this.panningStart.x + this.panningStart.left) + 'px';
      field.style.top = (0 + y - this.panningStart.y + this.panningStart.top) + 'px';
      this.setOffsets();
    }
    else if(this.lastDraw)
    {
      var cc = this.getCheckCoords(ev);
      if(cc.x != this.lastDraw.x || cc.y != this.lastDraw.y)
      {
        if(this.lastDraw.s == '+')
          this.setCheck(this.curGen, cc.x, cc.y);
        else
          this.clearCheck(this.curGen, cc.x, cc.y);
        this.lastDraw.x = cc.x;
        this.lastDraw.y = cc.y;
        this.draw();
      }
    }
  },

  setOffsets: function()
  {
    var field = $('#field')[0];
    var shiftX = parseInt(field.style.left) - this.panningStart.left;
    var shiftY = parseInt(field.style.top) - this.panningStart.top;
    this.offsetX = this.panningStart.offX + Math.round(-shiftX / this.checkSize);
    this.offsetY = this.panningStart.offY + Math.round(-shiftY / this.checkSize);
  },

  getCheckCoords: function(ev)
  {
    var x = ev.offsetX || ev.layerX;
    var y = ev.offsetY || ev.layerY;
    var frame = $('#frame')[0];
    var cx = Math.floor((x - frame.clientWidth / 2) / this.checkSize + 0.5) + this.offsetX;
    var cy = Math.floor((y - frame.clientHeight / 2) / this.checkSize + 0.5) + this.offsetY;
    return { x: cx, y: cy };
  },

  stepCode: function()
  {
    var code = '';
    for(var y0 in this.curGen)
    {
      y0 = parseInt(y0);
      var xList = [];
      for(var x0 in this.curGen[y0])
      {
        x0 = parseInt(x0);
        xList.push(x0 < 0 ? x0 : '+' + x0);
      }
      if(xList.length)
        code += (y0 < 0 ? y0 : '+' + y0) +
          (xList.length > 1 ? '(' + xList.join('') + ')' : xList.join(''));
    }
    return code;
  },

  /*
    Helper functions for field operations
  */
  hasCheck: function(field, x, y)
  {
    return field[y] ? field[y][x] : 0;
  },

  setCheck: function(field, x, y)
  {
    if(typeof(field[y]) == 'undefined')
      field[y] = {};
    field[y][x] = 1;
  },

  clearCheck: function(field, x, y)
  {
    if(field[y])
      delete field[y][x];
  },

  toggleCheck: function(field, x, y)
  {
    if(this.hasCheck(field, x, y))
      this.clearCheck(field, x, y);
    else
      this.setCheck(field, x, y);
  },

  /*
    Init
  */
  init: function(area,birth, overcrowding,loneliness)
  {
      if (area != "" && birth != "" && overcrowding != "" && loneliness != "") {
          this.curGen = JSON.parse(area.replace(/&quot;/g, '"'));
          this.birth = birth;
          this.overcrowding = overcrowding;
          this.loneliness = loneliness;
      }
      this.newInit();
  },
  
  newInit: function () {
      // Bind handlers to controls
      $('#tbStep').click(function () { LifeGame.stop(); LifeGame.step(); return false; });
      $('#tbStart').click(function () { LifeGame.start(); return false; });
      $('#tbStop').click(function () { LifeGame.stop(); return false; });
      $('#tbZoomIn').click(function () { LifeGame.zoom(1); return false; });
      $('#tbZoomOut').click(function () { LifeGame.zoom(-1); return false; });
      $('#tbCenter').click(function () { LifeGame.center(); return false; });
      $('#tbReset').click(function () { LifeGame.reset(true); return false; });
      $('#tbApply').click(function () {
          var b = $('#birth').val();
          var o = $('#overcrowding').val();
          var l = $('#loneliness').val();
          LifeGame.applyRules(b, o, l); return false;
      });
      $('#tbSaveAs').click(function () { LifeGame.save(); return false; });
      $('#tbSave').click(function () { LifeGame.resave(); return false; });

      // Bind field mouse handlers
      $('#evcap')[0].onmousedown = function (ev) { LifeGame.frameMouseDown(ev || window.event); }
      $('#evcap')[0].onmouseup = function (ev) { LifeGame.frameMouseUp(ev || window.event); }
      $('#evcap')[0].onmouseout = function (ev) { LifeGame.frameMouseUp(ev || window.event); }
      $('#evcap')[0].onmousemove = function (ev) { LifeGame.frameMouseMove(ev || window.event); }

      this.updateControls();
      this.draw();
  }
  
  
};

