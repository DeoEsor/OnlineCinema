import { ApolloClient, InMemoryCache } from "@apollo/client";

export const client  = new ApolloClient({
  uri: "https://api-sa-east-1.graphcms.com/v2/cl4qcqy953vhg01xl3cyebfqh/master",
  cache: new InMemoryCache()
})