import * as signalR from "@microsoft/signalr";

const logPrefix = '[SignalR]';

export const setupConnection = (onConnected: Function) => {
    let connection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:5135/observation-hub')
        .withAutomaticReconnect()
        .build();
    connection
        .start()
        .then(() => { onConnected(); console.log(logPrefix, 'Socket Connected.'); })
        .catch((err: Error) => console.error(logPrefix, err));
    connection.onclose = () => {
        console.log(logPrefix, 'Socket Disconnected.');
    };
    return connection;
}