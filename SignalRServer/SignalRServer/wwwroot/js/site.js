// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
const conn = new signalR.HubConnectionBuilder()
    .withUrl("/realTime")
    .configureLogging(signalR.LogLevel.Information)
    .build();

conn.on("ReceiveMessage", (user, message) => {
    const encodedMsg = "Name: " + user + " Time: " + message;
    const li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

//document.getElementById("sendButton").addEventListener("click", event => {
//    const user = document.getElementById("userInput").value;
//    const message = document.getElementById("messageInput").value;
//    conn.invoke("SendMessage", user, message).catch(err => console.error(err.toString()));
//    event.preventDefault();
//});

conn.start().catch(err => console.error(err.toString()));