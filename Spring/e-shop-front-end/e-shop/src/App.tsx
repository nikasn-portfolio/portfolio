import './App.css';
import Content from './routes/ProductListPage';
import Footer from './components/Footer';
import LanguageSelector from './components/LanguageSelector';
import Navbar from './components/Navbar';


function App() {
  return (
    <div className="App">
      <Navbar />
      <Content />
      <Footer />
      
    </div>
  );
}

export default App;