# gRPC Training Project with .NET 8

## What is gRPC?

gRPC (Google Remote Procedure Call) is a modern, high-performance RPC framework that can run in any environment. It was designed by Google to facilitate efficient communication between services in microservices architectures. Here are some key features of gRPC:

- **Protocol Buffers**: gRPC uses Protocol Buffers (protobuf) as its Interface Definition Language (IDL). Protobuf is a method of serializing structured data which is language-neutral, platform-neutral, and extensible. This allows for efficient data serialization and deserialization.

- **HTTP/2**: gRPC leverages HTTP/2, which provides several advantages over HTTP/1.1:
  - **Multiplexing**: Multiple gRPC calls can be sent over a single HTTP/2 connection, reducing latency and improving throughput.
  - **Header Compression**: HTTP/2 uses HPACK for header compression, reducing overhead.
  - **Bidirectional Streaming**: Allows for both client-to-server and server-to-client streaming, enabling more complex communication patterns like real-time updates.

- **Language Agnostic**: gRPC supports multiple programming languages, making it versatile for polyglot environments. This means services written in different languages can communicate seamlessly.

- **Strong Typing**: With protobuf, the service definitions are strongly typed, which helps in catching errors at compile-time rather than runtime.

- **Efficient**: Due to its use of protobuf and HTTP/2, gRPC is known for its efficiency in terms of network usage and performance, especially beneficial for mobile and resource-constrained environments.

- **Security**: gRPC supports TLS encryption out of the box, ensuring secure communication between services.

## Project Description

This project, named **OrderingSystemUsinggRPC**, demonstrates the implementation of a gRPC-based microservices architecture using .NET 8. It simulates an ordering system where different services interact to process an order. The project consists of four main components:

- **OrderingSystemUsinggRPC.Api**: This is the Web API (Ordering Service) that handles REST requests from users to place orders. It receives requests containing order details like `Order Id`, `User Id`, `Item Id`, `Price`, and `Quantity`.

- **OrderingSystemUsinggRPC.OrderingService**: This service acts as a bridge, communicating via gRPC with both the Payment and Inventory services to process the order.

- **OrderingSystemUsinggRPC.PaymentService**: This gRPC service is responsible for handling payment transactions. It receives requests from the Ordering Service to deduct the user's balance based on `User Id` and `Total Price`, and it returns a `Status` indicating the success or failure of the transaction.

- **OrderingSystemUsinggRPC.InventoryService**: This gRPC service manages the inventory. It receives requests from the Ordering Service to update the inventory by deducting the quantity of the ordered item based on `Item Id` and `Quantity`, and it returns a `Status`.

### System Flow

1. **REST Request**: A user sends a REST request to the `OrderingSystemUsinggRPC.Api` with order details.
2. **gRPC Communication**: The API forwards the necessary details to the `OrderingSystemUsinggRPC.OrderingService`.
3. **Payment Deduction**: The Ordering Service sends a gRPC request to the `OrderingSystemUsinggRPC.PaymentService` to deduct the user's balance.
4. **Inventory Update**: Simultaneously, it sends a gRPC request to the `OrderingSystemUsinggRPC.InventoryService` to update the inventory.
5. **Response**: The responses from both services are aggregated, and a final status is returned to the user via the API.

## Getting Started

To run this project, you'll need:

- .NET 8 SDK installed
- gRPC tools for .NET

### Steps:

1. Clone the repository:
   ```bash
   git clone [https://github.com/uosefahmed22/OrderingSystemUsingGRPC]
   ```

2. Navigate to the project directory:
   ```bash
   cd OrderingSystemUsinggRPC
   ```

3. Build and run the services:
   ```bash
   dotnet run --project OrderingSystemUsinggRPC.Api
   dotnet run --project OrderingSystemUsinggRPC.OrderingService
   dotnet run --project OrderingSystemUsinggRPC.PaymentService
   dotnet run --project OrderingSystemUsinggRPC.InventoryService
   ```

## Project Structure

- **OrderingSystemUsinggRPC.Api**: Contains the Web API and gRPC client logic to interact with Payment and Inventory services.
- **OrderingSystemUsinggRPC.OrderingService**: Acts as the central service coordinating between the API, Payment, and Inventory services.
- **OrderingSystemUsinggRPC.PaymentService**: gRPC service to handle user balance deductions.
- **OrderingSystemUsinggRPC.InventoryService**: gRPC service to handle inventory updates.
