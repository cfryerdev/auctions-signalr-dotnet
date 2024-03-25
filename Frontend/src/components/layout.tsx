import * as React from 'react';
import { Link } from "react-router-dom";
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import GlobalStyles from "@mui/material/GlobalStyles";
import Container from '@mui/material/Container';
import { createTheme } from '@mui/material/styles';
import { makeStyles } from '@mui/styles';

const theme = createTheme();
const useStyles = makeStyles(() => ({
  container: {
    [theme.breakpoints.up('sm')]: {
      paddingLeft: '20px',
      paddingRight: '20px',
    },
    [theme.breakpoints.down('xs')]: {
      paddingLeft: '0px',
      paddingRight: '0px',
    },
  },
}));

interface LayoutProps {
    children: any;
}

const GlobalTheme = () => {
  return (
    <GlobalStyles
      styles={(theme) => ({
        body: { padding: 0, margin: 0 }
      })}
    />
  )
}

const Layout = ({ children }: LayoutProps) => {
  const classes = useStyles();
  return (
    <>
      <GlobalTheme />
      <>
        <AppBar position="static">
          <Toolbar>
            <IconButton
              size="large"
              edge="start"
              color="inherit"
              aria-label="menu"
              sx={{ mr: 2 }}
            >
              <MenuIcon />
            </IconButton>
            <Link to="/" style={{ flexGrow: 1, color: '#fff', textDecoration: 'none' }}>
              <Typography variant="h6" component="div" >
                Auctioneer
              </Typography>
            </Link>
            <Button color="inherit">Login</Button>
          </Toolbar>
        </AppBar>
      </>
      <Container className={classes.container}>{ children }</Container>
    </>
  )
}

export default Layout;