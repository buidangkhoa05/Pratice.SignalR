import * as signalR from "@microsoft/signalr";

const URL = "http://localhost:5083/hub"; //or whatever your backend port is
const serialId = 1001;
class Connector {
  private connection: signalR.HubConnection;
  static instance: Connector;

  public events: (
    onMessageReceived: (username: string, message: string) => void
  ) => void;

  public dataUpdateEvent: (
    onDataUpdated: (dataType: string, data: string) => void
  ) => void;

  constructor() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`${URL}?searialId=${serialId}`, {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .withAutomaticReconnect()
      .build();

    console.log(`BaseURL of HUB connection:` + this.connection.baseUrl);
    this.connection.start().catch((err) => document.write(err));

    this.events = (onMessageReceived) => {
      this.connection.on("messageReceived", (username, message) => {
        onMessageReceived(username, message);
      });
    };

    this.dataUpdateEvent = (onDataUpdated) => {
      this.connection.on("dataUpdated", (dataType, data) => {
        onDataUpdated(dataType, data);
      });
    };
  };

  public newMessage = (messages: string) => {
    this.connection
      .send("newMessage", "foo", messages)
      .then((x) => console.log("sent"));
  };

  public static getInstance(): Connector {
    if (!Connector.instance) Connector.instance = new Connector();
    return Connector.instance;
  }
}
export default Connector.getInstance;
