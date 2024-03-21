import App from './app';
import * as ReactDOMClient from "react-dom/client";

import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';

const rootElement = document.getElementById("root");
const root = ReactDOMClient.createRoot(rootElement!);

root.render(<App />);