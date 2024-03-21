import { BrowserRouter } from "react-router-dom";
import { QueryClient, QueryClientProvider } from 'react-query';
import Layout from "./components/layout";
import Routing from "./routes";

const queryClient = new QueryClient();

const HostRouter = () => {
  return (
    <BrowserRouter>
      <QueryClientProvider client={queryClient}>
      <Layout>
        <Routing />
      </Layout>
      </QueryClientProvider>
    </BrowserRouter>
  );
};

export default HostRouter;