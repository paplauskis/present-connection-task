import { useState } from 'react'
import { createPackage } from '../apiCalls/packageApi'
import { useNavigate } from 'react-router-dom'
import PackageForm from '../components/packageForm/PackageForm'

export default function CreatePackagePage() {
  const [sender, setSender] = useState({ name: '', address: '', phone: '' })
  const [recipient, setRecipient] = useState({ name: '', address: '', phone: '' })
  const [loading, setLoading] = useState(false)
  const [message, setMessage] = useState('')
  const navigate = useNavigate()

  const handleChange = (e, type) => {
    const { name, value } = e.target
    if (type === 'sender') {
      setSender({ ...sender, [name]: value })
    } else {
      setRecipient({ ...recipient, [name]: value })
    }
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    setMessage('')

    const payload = { sender, recipient }

    try {
      setLoading(true)

      const result = await createPackage(payload)

      if (result.error) {
        setMessage(result.error)
      } else {
        setMessage(`Package created: ${result.trackingNumber}`)
        setSender({ name: '', address: '', phone: '' })
        setRecipient({ name: '', address: '', phone: '' })
      }
    } catch (err) {
      setMessage('Error creating package. Try again.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="create-package-page">
      <h1>Create New Package</h1>

      <PackageForm
        sender={sender}
        recipient={recipient}
        loading={loading}
        message={message}
        onChange={handleChange}
        onSubmit={handleSubmit}
      />

      <button onClick={() => navigate('/')}>Back to List</button>
    </div>
  )
}