const WebSocket = require('ws');
const HTTP = require('http');

var players = [];
var id = 1;

HTTP.createServer((req, res) => {
    if (req.method == 'POST') {
        let body = [];
        var jsonbody;
        req.on('data', (chunk) => {
            body.push(chunk);
        }).on('end', () => {
            body = Buffer.concat(body).toString();
            jsonbody = JSON.parse(body);
            if (jsonbody.pass == "NuFiCapra") {
                players.push({ pid: id, username: jsonbody.user });
                console.log(`Player ${jsonbody.user} has joined!\n`);
                res.writeHead(200, { 'Content-type': 'text/plain' })
                res.write(`${id}`);
                res.end();
                id++;
            }
            else {
                res.writeHead(404, { 'Content-type': 'text/plain' });
                res.write('Nu stiu ce vroiai sa faci capra ce esti...');
                res.end();
            }
        });
    }
}).listen(7515);

var queue = [];

const socket = new WebSocket.Server({
    host: '192.168.1.11',
    port: 7514,
});

var connections = [];
var match = [];

socket.on('connection', (ws) => {
    ws.on('message', (message) => {
        if(message.startsWith('iam ')) {
            connections.push({id: message.substring(4), socket: ws});
        }
        if (message.startsWith('match ')) {
            console.log(`Player ${players.find((item) => { return item.pid == message.substring(6) }).username} wants to match!`)
            queue.push(message.substring(6));
            if (queue.length > 1) { // extend with an MMR system maybe
                let id1 = queue[0], id2 = queue[1];
                let user1 = players.find((item) => { return item.pid == id1 }).username, user2 = players.find((item) => { return item.pid == id2 }).username;
                console.log(`Matched players ${user1} and ${user2}\n`);
                connections.find((item) => { return item.id == id1 }).socket.send(`${id1}|${user1}|${id2}|${user2}`);
                connections.find((item) => { return item.id == id2 }).socket.send(`${id1}|${user1}|${id2}|${user2}`);
                match[id1] = id2;
                queue.pop();
                queue.pop();
            }
        }
        else if(message.startsWith('move ')) {
            var endindex;
            for(var i = 5; i < message.length; i++) {
                if(message[i] == ',') {
                    endindex = i;
                    break;
                }
            }
            let otherid = match[message.substring(5, endindex)];
            connections.find((item) => { return item.id == otherid}).socket.send(message.substring(5));
        }
    });
});

socket.on('error', (err) => {
    console.log(err);
})