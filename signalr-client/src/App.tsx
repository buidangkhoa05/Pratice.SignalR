import React, { useEffect, useState } from "react";
import "./App.css";
import Connector from "./signalr-connection";

function App() {
  const { newMessage, events, dataUpdateEvent } = Connector();
  const [message, setMessage] = useState("initial value");

  useEffect(() => {
    // events((_, message) => setMessage(message));
    dataUpdateEvent((dataType, data) => setMessage(dataType + ":" + data));
    console.log("useEffect");
  });

  return (
    <div className="App">
      <span>
        message from signalR: <span style={{ color: "green" }}>{message}</span>{" "}
      </span>
      <br />
      <button onClick={() => newMessage(new Date().toISOString())}>
        send date{" "}
      </button>
    </div>
  );
}
export default App;
