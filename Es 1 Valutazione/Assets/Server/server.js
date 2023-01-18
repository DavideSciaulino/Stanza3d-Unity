const WebSocket = require('ws');

//creo il server alla porta 8080
const wss = new WebSocket.Server({port: 8080}, ()=>{
    console.log("Server Avviato Correttamente");
});

//server in ascolto
wss.on("listening",()=>{
    console.log("Server in ascolto");
});

//se un client si connette e manda un messaggio
wss.on("connection",(ws)=>{
  console.log("Client Connesso");
    ws.on("message",(data)=>{
        console.log('Ricevuto: ' + data);
        ws.send(data);
    });
});