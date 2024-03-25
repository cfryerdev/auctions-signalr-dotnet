import { useEffect, useState } from "react";
import { usePastAuctions, useTodaysAuctions, useFutureAuctions } from '../services/auction-service';
import { setupConnection } from "../services/socket-service";
import AuctionCard from "../components/auction-card";
import Loading from "../components/loading";

import Tab from '@mui/material/Tab';
import TabContext from '@mui/lab/TabContext';
import TabList from '@mui/lab/TabList';
import TabPanel from '@mui/lab/TabPanel';
import Box from "@mui/material/Box";


const TestPage = () => {
  const [isConnected, setIsConnected] = useState<Boolean>(false);
  const [liveAuctions, setLiveAuctions] = useState<any[]>([]);
  const [value, setValue] = useState('1');

  const { data: pastAuctions, isFetched: pastFetched } = usePastAuctions();
  const { data: todaysAuctions, isFetched: todayFetched } = useTodaysAuctions();
  const { data: futureAuctions, isFetched: futureFetched } = useFutureAuctions();

  const handleChange = (event: React.SyntheticEvent, newValue: string) => {
    setValue(newValue);
  };

  useEffect(() => {
    const connection = setupConnection(() => { setIsConnected(true); });
    connection.on("UpdateTodaysActiveAuctions", (activeAuctions: object[]) => { setLiveAuctions(activeAuctions); });

    return () => {
      connection.stop();
    };
  }, []);

  return (
    <>
      <TabContext value={value}>
        <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
          <TabList onChange={handleChange} aria-label="lab API tabs example">
            <Tab label="Today" value="1" />
            <Tab label="Future" value="2" />
            <Tab label="Past" value="3" />
          </TabList>
        </Box>
        <TabPanel value="1">
          <h3>Currently active auctions</h3>
          {isConnected ? <>
            {liveAuctions.map((item, i) => {
              return <AuctionCard key={i} auction={item} hasBid={true} />
            })}
          </> : <Loading />}
          <h3>Today's auctions</h3>
          {todayFetched ? <>
            {todaysAuctions != undefined && Array.isArray(todaysAuctions) && todaysAuctions.map((item, i) => {
              return <AuctionCard key={i} auction={item} hasBid={false} />
            })}
          </> : <Loading />}
        </TabPanel>
        <TabPanel value="2">
          <h3>Upcoming auctions</h3>
          {futureFetched ? <>
            {futureAuctions != undefined && Array.isArray(futureAuctions) && futureAuctions.map((item, i) => {
              return <AuctionCard key={i} auction={item} hasBid={false} />
            })}
          </> : <Loading />}
        </TabPanel>
        <TabPanel value="3">
          <h3>Previous auctions</h3>
          {pastFetched ? <>
            {pastAuctions != undefined && Array.isArray(pastAuctions) && pastAuctions.map((item, i) => {
              return <AuctionCard key={i} auction={item} hasBid={false} />
            })}
          </> : <Loading />}
        </TabPanel>
      </TabContext>
    </>)
}

export default TestPage;