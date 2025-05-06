import React from 'react';
import ReactDOM from 'react-dom/client';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { CssBaseline, ThemeProvider, createTheme } from '@mui/material';
import TeamList from './pages/TeamList';

const queryClient = new QueryClient();
const theme = createTheme({
  palette: {
    primary: { main: '#007bff' },
    background: { default: '#e9ecef' },
  },
  breakpoints: {
    values: { xs: 0, sm: 600, md: 960, lg: 1280, xl: 1920 },
  },
});

ReactDOM.createRoot(document.getElementById('root')!).render(
  <QueryClientProvider client={queryClient}>
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <TeamList />
    </ThemeProvider>
  </QueryClientProvider>
);